using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    /// <summary>
    /// /using query string values with no hints
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    // [HttpPost("register")] //account/register
    // public async Task<ActionResult<AppUser>> Register(string username, string password)
    // {
    //     using var hmac = new HMACSHA512();//not injected, just used and disposed immediately upon loss of scope.

    //     var user = new AppUser
    //     {
    //         UserName = username,
    //         PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
    //         PasswordSalt = hmac.Key
    //     };

    //     context.Users.Add(user);
    //     await context.SaveChangesAsync();

    //     return Ok(user);
    // }


    [HttpPost("register")] //account/register
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
    {
        //make sure user doesn't already exist
        if (await UserExists(registerDto.Username)) return BadRequest("Username Taken");

        using var hmac = new HMACSHA512();//not injected, just used and disposed immediately upon loss of scope.

        return Ok();
        // var user = new AppUser
        // {
        //     UserName = registerDto.Username,
        //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        //     PasswordSalt = hmac.Key
        // };

        // context.Users.Add(user);
        // await context.SaveChangesAsync();

        // return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await 
            context.Users.FirstOrDefaultAsync(x => 
            x.UserName == loginDto.Username.ToLower());

        if (user == null) return Unauthorized("Invalid username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }
    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}
