using System.Net;
using Domain.Auth.Interfaces;
using Domain.Cargo.Interfaces;
using Domain.Company.Interfaces;
using Domain.Container.Interfaces;
using Domain.Ship.Interfaces;
using Domain.User.Interfaces;
using Infrastructure.Database.Repositories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Web.Auth.Services;
using Web.Companies.Services;
using Web.Users.Services;

namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.Events.OnRedirectToLogin = context =>
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return Task.CompletedTask;
                };
            });
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(20); });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICompanyService, CompanyService>();
        builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
        builder.Services.AddScoped<IShipRepository, ShipRepository>();
        builder.Services.AddScoped<ICargoRepository, CargoRepository>();
        builder.Services.AddScoped<IContainerShipRepository, ContainerShipRepository>();
        builder.Services.AddScoped<IContainerRepository, ContainerRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        app.MapControllers();

        app.Run();
    }
}