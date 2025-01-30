using System;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

//switched from direct dbcontext when implementing repository method
//public class UsersController(DataContext context) : BaseApiController
//public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
//removed mapper when we started mapping in the repo
[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
{
    #region 
    ////////////////////synchronous code/////////////////////////
    // [HttpGet]
    // public ActionResult<IEnumerable<AppUser>> GetUsers()
    // {
    //     var users = context.Users.ToList();
    //     return Ok(users);
    // }

    // [HttpGet("{id:int}")] //api/users/1
    // public ActionResult<AppUser> GetUser(int id)
    // {
    //     var user = context.Users.Find(id);
        
    //     if(user == null) return NotFound();
        
    //     return Ok(user);
    // }
    #endregion

    ///////////////// asynchronous code ////////////////////
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        // var users = await userRepository.GetUsersAsync();
        // var usersToReturn = mapper.Map<IEnumerable<MemberDto>>(users);
        // return Ok(usersToReturn);
        // that was old way pre mapper in repo
        var users = await userRepository.GetMembersAsync();
        return Ok(users);
    }

    //[HttpGet("{id:int}")] //api/users/1 -this used dbcontext and went to id
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        // var user = await userRepository.GetUserByUsernameAsync(username);        
        // if(user == null) return NotFound();        
        // var userToReturn = mapper.Map<MemberDto>(user);
        // return Ok(userToReturn);
        //^^ old way pre mapper in repo
        
        var user = await userRepository.GetMemberAsync(username);        
        if(user == null) return NotFound();        
        return Ok(user);
    }


    //////////////////////////////////////////////////////
    ///after all that we add the project to github
    ///go to the dotnet terminal and add the gitignore file
}
