using System.Text;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("conn")); //go to appsettings.Development.json for the config
            //off to the terminal to use dotnet ef migrations add InitialCreate -o Data/Migrations
            //then we run dotnet ef database update 
            //that will make the database by running the migration
            //that command also adds the migrations table to the db and adds the records for initial migration
            //next we make a controller 
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //services.AddSingleton //this keeps one instance across the app
        services.AddScoped<ITokenService, TokenService>(); //scoped allows injection to be in charge of the lifecycle
        
        return services;
    }
}
