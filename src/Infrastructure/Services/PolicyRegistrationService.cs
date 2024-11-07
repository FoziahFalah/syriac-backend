using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using SyriacSources.Backend.Infrastructure.Data;

namespace SyriacSources.Backend.Infrastructure.Services;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();

        await initialiser.SaveApplicationPolicies(app);
    }
}


public class PolicyRegistrationService
{


    private readonly IApplicationPermissionService _permissionsService;
    private readonly IAuthorizationPolicyProvider _policyProvider;
    public PolicyRegistrationService(IApplicationPermissionService permissionsService, IAuthorizationPolicyProvider policyProvider)
    {
        _permissionsService = permissionsService;
        _policyProvider = policyProvider;
    }

    public async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        // Fetch policies from your database or other data source
        var policies = await _permissionsService.FetchPoliciesAsync();

        if(policies.Any())
        {
            var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
            foreach(var pol in policies)
            {
                
            }
        }
        if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase) &&
            int.TryParse(policyName.Substring(POLICY_PREFIX.Length), out var age))
        {
            
            policy.AddRequirements(new MinimumAgeRequirement(age));
            return Task.FromResult(policy.Build());
        }

        return Task.FromResult<AuthorizationPolicy>(null);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Fetch policies from your database or other data source
        var policies = await _permissionsService.FetchPoliciesAsync();

        // Register policies dynamically
        foreach (var policy in policies)
        {
            _policyProvider.AddPolicy(policy, policyOptions =>
                policyOptions.RequireClaim("policies", policy));
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Clean-up logic, if needed
        return Task.CompletedTask;
    }
}
