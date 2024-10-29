using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Constants;
using SyriacSources.Backend.Infrastructure.Data;
using SyriacSources.Backend.Infrastructure.Data.Interceptors;
using SyriacSources.Backend.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        string secretKey = configuration.GetSection("JWT:Secret").Get<string>() ?? throw new InvalidOperationException("JWT secret must not be null.");


        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = false,
                 ValidateAudience = false,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 RequireExpirationTime = true,
                 ValidIssuer = configuration.GetSection("JWT:ValidIssuer").Get<string>(),
                 ValidAudience = configuration.GetSection("JWT:ValidAudience").Get<string>(),
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(secretKey))))
             };
         });

        services.AddAuthorizationBuilder();

        services
            .AddIdentityCore<ApplicationUser>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        services.AddSingleton(TimeProvider.System);

        services.AddScoped<CurrentUser>();

        //Registering Services
        services.AddTransient<IIdentityService, IdentityUserService>();

        services.AddScoped<IApplicationUserService, ApplicationUserService>();

        services.AddScoped<IApplicationUserRoleService, ApplicationUserRoleService>();

        services.AddScoped<IApplicationRoleService, ApplicationRoleService>();

        services.AddTransient<ITokenService, TokenService>();

        services.Configure<JWTToken>(configuration.GetSection("JWT"));

        services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator))
            );

        return services;
    }
}
