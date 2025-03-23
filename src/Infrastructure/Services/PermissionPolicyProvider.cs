using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity;
public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    /// <summary>
    /// Retrieves an authorization policy by name. If not found in memory, checks the database.
    /// </summary>
    /// <param name="policyName">The name of the policy being requested.</param>
    /// <returns>
    /// An <see cref="AuthorizationPolicy"/> if found; otherwise, null.
    /// </returns>
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);

        if (policy is null)
        {
            var permissions = policyName;

            policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermisssionRequirement(permissions))
                .Build();
        }

        return policy;
    }
}
