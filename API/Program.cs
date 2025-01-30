using API.Extensions;
using API.Middleware;

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

app.Run();
