using System;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtenstions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
       services.AddControllers();
       services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));

        });
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
       services.AddCors();
       services.AddScoped<ITokenService, TokenService>();
       services.AddScoped<IUserRepository,UserRepository>();
       services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

       return services;
    }
}