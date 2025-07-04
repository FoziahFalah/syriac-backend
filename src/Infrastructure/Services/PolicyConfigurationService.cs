﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using SyriacSources.Backend.Domain.Constants;
using SyriacSources.Backend.Infrastructure.Data;

namespace SyriacSources.Backend.Infrastructure.Services;
public class PolicyConfigurationService
{
    private readonly IApplicationPermissionService _applicationPermissions;

    public PolicyConfigurationService(IApplicationPermissionService applicationPermissions)
    {
        _applicationPermissions = applicationPermissions;
    }


    /// <summary>
    /// Adds authorization policies to the provided <see cref="AuthorizationOptions"/>.
    /// This includes both pre-defined policies and dynamically retrieved policies from the database.
    /// </summary>
    /// <param name="options">The authorization options to which policies will be added.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddPoliciesAsync(AuthorizationOptions options)
    {
        // Add pre-defined policies
        options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator));

        // Retrieve policies from the database asynchronously
        var policies = await _applicationPermissions.FetchPoliciesAsync();

        // Add policies from the database
        if(policies != null)
        {
            foreach (var policy in policies)
            {
                options.AddPolicy(policy, policyOptions =>
                    policyOptions.RequireClaim(CustomClaimTypes.Permission, policy)); // Custom requirement
            }
        }
    }
}
