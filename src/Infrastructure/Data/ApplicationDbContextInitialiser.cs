using System.Runtime.InteropServices;
using SyriacSources.Backend.Domain.Constants;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SyriacSources.Backend.Application.Common.Extensions;
using static System.Formats.Asn1.AsnWriter;
using SyriacSources.Backend.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace SyriacSources.Backend.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();

        await initialiser.RegisterApplicationPolicies(app);
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IApplicationUserService _appUserService;
    private readonly IApplicationRoleService _appRoleService;
    private readonly IApplicationUserRoleService _appUserRoleService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationPermissionService _appPermissionService;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, IApplicationPermissionService appPermissionService, IApplicationUserService appUserService, IApplicationUserRoleService userRoleService, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IApplicationRoleService appRoleService)
    {
        _logger = logger;
        _context = context;
        _appUserRoleService = userRoleService;
        _userManager = userManager;
        _appRoleService = appRoleService;
        _appUserService = appUserService;
        _appPermissionService = appPermissionService;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var token = new CancellationToken();

        ApplicationRole administratorRole = new ApplicationRole { NormalizedRoleName = Roles.Administrator.NormalizeString(), NameAR = "مدير النظام" , NameEN = Roles.Administrator.NormalizeString(), CreatedBy = "S"};


        var roles = await _appRoleService.GetRolesAsync(token);

        if (roles.All(r => r.NormalizedRoleName != administratorRole.NormalizedRoleName))
        {
            await _appRoleService.CreateAsync(administratorRole, token);
        }
        else
        {
            administratorRole = roles.Where(x => x.NormalizedRoleName == Roles.Administrator.NormalizeString()).Single();
        }

        var user = new Contributor
        {
            FullNameAR = "مدير نظام",
            FullNameEN = Roles.Administrator,
            EmailAddress = "admin@local"
        };


        //Create identity user
        var administrator = new ApplicationUser { UserName = user.EmailAddress, Email = user.EmailAddress,Contributor = user};

        IdentityResult identityUser;

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            //Creating 
            identityUser =  await _userManager.CreateAsync(administrator, "Aa@123456");
            if (!identityUser.Succeeded)
            {
                var errors = identityUser.Errors.Select(x=>x.Description).Aggregate((i, j) => i + "\n " + j);
                _logger.LogError(errors);
                return;
            }
        }

        //Map Contributor to role
        if (!string.IsNullOrWhiteSpace(administratorRole.NormalizedRoleName))
        {
            ///Add To Role
            var isInRole = await _appUserRoleService.IsInRoleAsync(administrator.ContributorId, administratorRole.Id, token);

            if (!isInRole)
            {
                var userRole = await _appUserRoleService.AddToRolesAsync(administrator.ContributorId, new List<int> { administratorRole.Id }, token);
                if (!userRole.Succeeded)
                {
                    var errors = userRole.Errors.Aggregate((i, j) => i + "\n " + j);
                    _logger.LogError(errors);
                    return;
                }
            }
        }
    }

    //public async Task RegisterApplicationPolicies(WebApplication app)
    //{
    //    var policies = new HashSet<string>();

    //    // Get all endpoints
    //    var endpoints = app.Services.GetRequiredService<EndpointDataSource>().Endpoints;

    //    foreach (var endpoint in endpoints)
    //    {
    //        // Check for metadata that includes the Authorize attribute
    //        var authorizeMetadata = endpoint.Metadata.GetMetadata<IAuthorizeData>();
    //        if (authorizeMetadata != null && !string.IsNullOrEmpty(authorizeMetadata.Policy))
    //        {
    //            policies.Add(authorizeMetadata.Policy);
    //        }
    //    }

    //    // Insert policies into the database if they do not exist
    //    foreach (var policy in policies)

    //        if (!await _context.ApplicationPermissions.AnyAsync(p => p.PolicyName == policy))
    //        {
    //            await _appPermissionService.CreatePolicy(policy, new CancellationToken());
    //        }
    //}

    public async Task RegisterApplicationPolicies(WebApplication app)
    {
        var endpoints = app.Services.GetServices<EndpointDataSource>().SelectMany(x => x.Endpoints).ToArray();

        foreach (var endpoint in endpoints)
        {
            var authorisation = (endpoint?.Metadata
                .Where(p => p.GetType() == typeof(AuthorizeAttribute)))?.Cast<AuthorizeAttribute>();
            
            if (authorisation == null)
                continue;

            foreach (var authoriseAttribute in authorisation)
            {
                if (authoriseAttribute.Policy != null)
                    await _appPermissionService.CreatePolicy(authoriseAttribute.Policy, new CancellationToken());
                    
            }
        }
    }
}
