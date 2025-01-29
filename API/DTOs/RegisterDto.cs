using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]//this data annotation 
    public required string Username {get;set;} //this required is for the compiler

    [Required]
    public required string Password {get;set;}

}
