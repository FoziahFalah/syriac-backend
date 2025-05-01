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
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using MediatR;

namespace SyriacSources.Backend.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider;

        var services = initialiser.GetRequiredService<ApplicationDbContextInitialiser>();

        var policScannerService = initialiser.GetRequiredService<IPolicyScannerService>();

        await services.InitialiseAsync();

        await services.SeedAsync();

        await services.SaveApplicationPoliciesAsync(policScannerService.DiscoverPolicies());

    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IApplicationUserService _appUserService;
    private readonly IApplicationRoleService _appRoleService;
    private readonly IApplicationUserRoleService _appUserRoleService;
    private readonly UserManager<IdentityApplicationUser> _userManager;
    private readonly IApplicationPermissionService _appPermissionService;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, IApplicationPermissionService appPermissionService, IApplicationUserService appUserService, IApplicationUserRoleService userRoleService, ApplicationDbContext context, UserManager<IdentityApplicationUser> userManager, IApplicationRoleService appRoleService)
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
        try
        {
            var token = new CancellationToken();

            // Add Role
            ApplicationRole administratorRole = new ApplicationRole { NormalizedRoleName = Roles.Administrator.NormalizeString(), NameAR = "مدير النظام", NameEN = Roles.Administrator.NormalizeString(), CreatedBy = "S" };

            var roles = await _appRoleService.GetRolesAsync(token);

            if (roles.All(r => r.NormalizedRoleName != administratorRole.NormalizedRoleName))
            {
                await _appRoleService.CreateAsync(administratorRole, token);
            }
            else
            {
                administratorRole = roles.Where(x => x.NormalizedRoleName == Roles.Administrator.NormalizeString()).Single();
            }

            var result = 0;

            var details = new
            {
                FullNameAR = "مدير نظام",
                FullNameEN = Roles.Administrator,
                Email = "admin@darah.org.sa",
                Username = "admin",
                CreatedBy = "S",
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                LastModifiedBy = "S"
            };

            //First Add Identity User
            IdentityResult identityUser;

            IdentityApplicationUser? administrator = await _userManager.Users.Where(x=> x.UserName == details.Username).FirstOrDefaultAsync();
            
            if (administrator == null)
            {
                //Create identity user
                administrator = new IdentityApplicationUser
                {
                    UserName = details.Username,
                    Email = details.Email,
                    NormalizedEmail = details.Email?.Normalize().ToUpperInvariant(),
                    NormalizedUserName = details.Email?.Split("@")[0].Normalize().ToUpperInvariant(),
                };

                identityUser = await _userManager.CreateAsync(administrator, "Aa@123456");
                
                if (!identityUser.Succeeded)
                {
                    var errors = identityUser.Errors.Select(x => x.Description).Aggregate((i, j) => i + "\n " + j);

                    _logger.LogError("Identity ApplicationUser not Created");
                    _logger.LogError(errors);
                    return;
                }
            }

            // Now Add User
            var user = await _context.ApplicationUsers.Where(x => x.Email == details.Email).SingleOrDefaultAsync();

            if (user == null)
            {
                user = new ApplicationUser
                {
                    //Id = administrator.Id,
                    FullNameAR = details.FullNameAR,
                    FullNameEN = details.FullNameEN,
                    Email = details.Email,
                    CreatedBy = details.CreatedBy,
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    LastModifiedBy = details.LastModifiedBy
                };

                _context.ApplicationUsers.Add(user);

              

                if (result <= 0)
                {
                    _logger.LogError("Default ApplicationUser Not Created");
                    return;
                }
            }

            var userId = result > 0 ? result : user.Id;

          

            //Map ApplicationUser to role
            if (!string.IsNullOrWhiteSpace(administratorRole.NormalizedRoleName))
            {
                ///Add To Roles
                var isInRole = await _appUserRoleService.IsInRoleAsync(administrator.Id, administratorRole.Id, token);

                if (!isInRole)
                {
                    var userRole = await _appUserRoleService.AddToRolesAsync(administrator.Id, new List<int> { administratorRole.Id }, token);
                    if (!userRole.Succeeded)
                    {
                        var errors = userRole.Errors.Aggregate((i, j) => i + "\n " + j);
                        _logger.LogError("Application Roles not assigned");
                        _logger.LogError(errors);
                        return;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task SaveApplicationPoliciesAsync(HashSet<string> policies )
    {
        // Insert policies into the database if they do not exist
        foreach (var policy in policies)
        {
            if (!await _context.ApplicationPermissions.AnyAsync(p => p.PolicyName == policy))
            {
                await _appPermissionService.CreatePolicy(policy, new CancellationToken());
            }
        }
    }
}
