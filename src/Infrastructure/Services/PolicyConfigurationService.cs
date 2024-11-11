using System;
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
