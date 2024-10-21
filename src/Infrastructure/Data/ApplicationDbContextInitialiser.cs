using System.Runtime.InteropServices;
using SyriacSources.Backend.Domain.Constants;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SyriacSources.Backend.Infrastructure.Services;
using SyriacSources.Backend.Application.Common.Interfaces;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SyriacSources.Backend.Application.Common.Models;

namespace SyriacSources.Backend.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
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

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, IApplicationUserService appUserService, IApplicationUserRoleService userRoleService, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IApplicationRoleService appRoleService)
    {
        _logger = logger;
        _context = context;
        _appUserRoleService = userRoleService;
        _userManager = userManager;
        _appRoleService = appRoleService;
        _appUserService = appUserService;
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

        ApplicationRole administratorRole = new ApplicationRole { NormalizedRoleName = "ADMINISTRATOR" , NameAR = "مدير النظام" , NameEN = "Administrator", CreatedBy = "S"};


        var roles = await _appRoleService.GetRolesAsync(token);

        if (roles.All(r => r.NormalizedRoleName != administratorRole.NormalizedRoleName))
        {
            await _appRoleService.CreateAsync(administratorRole, token);
        }
        else
        {
            administratorRole = roles.Where(x => x.NormalizedRoleName == "ADMINISTRATOR").Single();
        }

        var user = new Contributor
        {
            FullNameAR = "مدير نظام",
            FullNameEN = "Administrator",
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
}
