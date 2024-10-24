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

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

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

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

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
