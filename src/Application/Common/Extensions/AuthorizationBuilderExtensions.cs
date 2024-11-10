using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Common.Extensions;
public static class AuthorizationBuilderExtensions
{
    //public readonly IApplicationPermissionService _applicationPermissionService;
    //public AuthorizationBuilderExtensions(IApplicationPermissionService applicationPermissionService)
    //{
    //    _applicationPermissionService = applicationPermissionService;   
    //}

    /// <summary>
    /// Adds multiple policies to the AuthorizationBuilder.
    /// </summary>
    /// <param name="builder">The AuthorizationBuilder.</param>
    /// <returns>The AuthorizationBuilder with the added policies.</returns>
    public static AuthorizationBuilder AddCustomPolicies(this AuthorizationBuilder builder)
    {
        var serviceProvider = services.BuildServiceProvider();
        var authorizationConfigService = serviceProvider.GetRequiredService<IAuthorizationConfigurationService>();
        IApplicationPermissionService appPermission = new Applicaionpermiss
        // Add predefined policies
        builder.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
        builder.AddPolicy("CanManageSystem", policy => policy.RequireClaim("permissions", "manage-system"));

        // Example of adding additional policies dynamically from a database, etc.
        var dynamicPolicies = IApplicationPermissionService new List<string> { "CanViewReports", "CanEditUser" };

        foreach (var policy in dynamicPolicies)
        {
            builder.AddPolicy(policy, policyOptions =>
                policyOptions.RequireClaim("permissions", policy)); // Custom requirement for dynamic policies
        }

        return builder;
    }
}
