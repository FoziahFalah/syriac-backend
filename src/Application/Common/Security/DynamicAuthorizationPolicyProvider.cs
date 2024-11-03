using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SyriacSources.Backend.Application.Common.Models;

namespace SyriacSources.Backend.Application.Common.Security;
public class DynamicAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    private readonly PolicyManagementOptions _policyOptions;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IConfiguration _configuration;


    public DynamicAuthorizationPolicyProvider(
        IOptions<AuthorizationOptions> options,
        IOptions<PolicyManagementOptions> policyOptions,
        IHttpContextAccessor contextAccessor,
        IConfiguration configuration
        ) : base(options)
    {
        _policyOptions = policyOptions.Value;
        _contextAccessor = contextAccessor;
        _configuration = configuration;
    }


    public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        // Check static policies first
        var policy = await base.GetPolicyAsync(policyName);

        if (policy == null)
        {
            var policyService = _contextAccessor.HttpContext.RequestServices.GetService<PolicyManagementService>();
            var policyInfo = await policyService.FetchPolicy(policyName);
            if (policyInfo != null)
            {
                return policyInfo.ToAuthPolicy();
            }

            if (_policyOptions.AutoCreateMissingPolicies)
            {
                //initialize policy in the data storage
                var newPolicy = new AuthorizationPolicyInfo();
                newPolicy.Name = policyName;

                if (_policyOptions.PolicyNamesToConfigureAsAllowAnonymous.Contains(policyName))
                {
                    await policyService.CreatePolicy(newPolicy);
                    return newPolicy.ToAuthPolicy();
                }
                else if (_policyOptions.PolicyNamesToConfigureAsAnyAuthenticatedUser.Contains(policyName))
                {
                    newPolicy.RequireAuthenticatedUser = true;
                    await policyService.CreatePolicy(newPolicy);
                    return newPolicy.ToAuthPolicy();
                }
                else
                {
                    var allowedRoles = _policyOptions.AutoPolicyAllowedRoleNamesCsv.Split(',');

                    var roleList = new List<string>(allowedRoles);
                    newPolicy.AllowedRoles = roleList;
                    await policyService.CreatePolicy(newPolicy);

                    policy = new AuthorizationPolicyBuilder()
                        .RequireRole(allowedRoles)
                        .Build();
                }

                var logger = _contextAccessor.HttpContext.RequestServices.GetService<ILogger<DynamicAuthorizationPolicyProvider>>();
                logger.LogWarning($"policy named {policyName} was missing so auto creating it with default allowed roles");

            }

        }

        return policy;
    }
}
