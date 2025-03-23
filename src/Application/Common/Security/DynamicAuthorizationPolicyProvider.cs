using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;

namespace SyriacSources.Backend.Application.Common.Security;
public class DynamicAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    private readonly PolicyManagementOptions _policyOptions;
    private readonly IOptions<AuthorizationOptions> options;
    private readonly IApplicationPermissionService _permissionService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DynamicAuthorizationPolicyProvider> _logger;


    public DynamicAuthorizationPolicyProvider(
        ILogger<DynamicAuthorizationPolicyProvider> logger,
        IOptions<PolicyManagementOptions> policyOptions,
        IApplicationPermissionService permissionService,
        IHttpContextAccessor contextAccessor,
        IConfiguration configuration
        ) : base(options)
    {
        _policyOptions = policyOptions.Value;
        _logger = logger;
        _permissionService = permissionService;
        _contextAccessor = contextAccessor;
        _configuration = configuration;
    }


    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        // Check static policies first
        var policy = await base.GetPolicyAsync(policyName);

        if (policy == null)
        {
            var policyInfo = await _permissionService.FetchPolicyByName(policyName);

            if (policyInfo != null)
            {
                return policyInfo.ToAuthPolicy();
            }

            if (_policyOptions.AutoCreateMissingPolicies)
            {
                //initialize policy in the data storage
                var newPolicy = new AuthorizationPolicyInfo();
                newPolicy.Name = policyName;
                var allowedRoles = _policyOptions.AutoPolicyAllowedRoleNamesCsv.Split(',');

                if (_policyOptions.PolicyNamesToConfigureAsAllowAnonymous.Contains(policyName))
                {
                    await _permissionService.CreatePolicy(newPolicy);
                    return newPolicy.ToAuthPolicy();
                }
                else if (_policyOptions.PolicyNamesToConfigureAsAnyAuthenticatedUser.Contains(policyName))
                {
                    newPolicy.RequireAuthenticatedUser = true;
                    await _permissionService.CreatePolicy(newPolicy);
                    return newPolicy.ToAuthPolicy();
                }
                else
                {
                    //var allowedRoles = _policyOptions.AutoPolicyAllowedRoleNamesCsv.Split(',');

                    var roleList = new List<string>(allowedRoles);
                    newPolicy.AllowedRoles = roleList;
                    await _permissionService.CreatePolicy(newPolicy);

                    policy = new AuthorizationPolicyBuilder()
                        .RequireRole(allowedRoles)
                        .Build();
                }

                var logger = _contextAccessor.HttpContext.RequestServices.GetService<ILogger<DynamicAuthorizationPolicyProvider>>();
                _logger.LogWarning($"policy named {policyName} was missing so auto creating it with default allowed roles");
            }
        }

        return policy;
    }
}
