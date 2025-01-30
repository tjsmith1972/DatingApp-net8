using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//add application services
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build(); //this must come after all services area added...
app.UseMiddleware<ExceptionMiddleware>();

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

app.UseAuthentication();//first authenticate
app.UseAuthorization();//then authorize - BOTH before mapping controllers

app.MapControllers();

using var scope = app.Services.CreateScope();//app not running yet need scope
//for consuming services pre app
var services = scope.ServiceProvider;
try
{
    var context =  services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch (System.Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
