using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt=>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("conn")); //go to appsettings.Development.json for the config
    //off to the terminal to use dotnet ef migrations add InitialCreate -o Data/Migrations
    //then we run dotnet ef database update 
    //that will make the database by running the migration
    //that command also adds the migrations table to the db and adds the records for initial migration
    //next we make a controller 
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => 
    x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200",
                    "https://localhost:4200"));
//app.UseHttpsRedirection(); //turn this back on to force https based on http setting in launchSettings.json

app.UseAuthorization();

app.MapControllers();

app.Run();
