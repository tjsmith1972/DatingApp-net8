using System;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // /api/users this introduces the "/api/*" routing
public class UsersController(DataContext context) : ControllerBase
{
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
    ///////////////// asynchronous code ////////////////////
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id:int}")] //api/users/1
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user = await context.Users.FindAsync(id);
        
        if(user == null) return NotFound();
        
        return Ok(user);
    }


    //////////////////////////////////////////////////////
    ///after all that we add the project to github
    ///go to the dotnet terminal and add the gitignore file
}
