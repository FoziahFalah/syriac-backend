using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Constants;
using SyriacSources.Backend.Infrastructure.Data;
using SyriacSources.Backend.Infrastructure.Data.Interceptors;
using SyriacSources.Backend.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EventManager.Backend.Infrastructure.Services;
using Ganss.Xss;
using SyriacSources.Backend.Application.Languages;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection  AddInfrastructureServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        string secretKey = builder.Configuration.GetSection("JWT:Secret").Get<string>() ?? throw new InvalidOperationException("JWT secret must not be null.");

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

        
        services
            .AddIdentityCore<IdentityApplicationUser>()
            .AddUserManager<UserManager<IdentityApplicationUser>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();

        services.AddSingleton(TimeProvider.System);

        services.AddScoped<CurrentUser>();

        //Registering Services
        services.AddTransient<IIdentityApplicationUserService, IdentityApplicationUserService>();

        services.AddTransient<IApplicationUserService, ApplicationUserService>();

        services.AddTransient<IApplicationUserRoleService, ApplicationUserRoleService>();

        services.AddTransient<IApplicationRoleService, ApplicationRoleService>();

        services.AddTransient<IApplicationPermissionService, ApplicationPermissionService>();

        services.AddScoped<PolicyConfigurationService>();

        services.AddTransient<ITokenService, TokenService>();

        services.Configure<JWTToken>(builder.Configuration.GetSection("JWT"));

        services.Configure<JsonDelimiters>(builder.Configuration.GetSection("JsonDelimiters"));

        services.AddAuthorizationBuilder();

        services.Configure<PolicyManagementOptions>(builder.Configuration.GetSection("PolicyManagementOptions"));

        services.AddAutoMapper(typeof(LanguageDto));


        // For dynamically create policy if not exist
        //services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

        services.AddTransient<IHtmlSanitizer, HtmlSanitizer>();

        //services.AddTransient<IFileService, FileService>(file =>
        //{
        //    return new FileService(builder.Environment.WebRootPath, file.GetRequiredService<IHttpContextAccessor>());
        //});

        var origins = builder.Configuration.GetSection("AllowedOrigins").Value;

        if (!String.IsNullOrEmpty(origins))
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigins",
                policy =>
                {
                    policy.WithOrigins(origins.Split(";"))
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });
        }
        else
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigins",
                policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,
                ValidIssuer = builder.Configuration.GetSection("JWT:ValidIssuer").Get<string>(),
                ValidAudience = builder.Configuration.GetSection("JWT:ValidAudience").Get<string>(),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(secretKey))))
            };
        });

        services.AddAuthorization(options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var authorizationConfigService = serviceProvider.GetRequiredService<PolicyConfigurationService>();
                // Configure authorization options asynchronously
                authorizationConfigService.AddPoliciesAsync(options).GetAwaiter().GetResult();
            }
        );


        return services;
    }
}
