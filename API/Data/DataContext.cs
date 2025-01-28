using System;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

//this DataContext class could have been called anything.
//it was created first, then program.cs has to add the dbcontext service
//follow to there next
public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
    
}
