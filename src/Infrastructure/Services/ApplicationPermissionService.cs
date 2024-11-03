using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SyriacSources.Backend.Infrastructure.Services;
public class ApplicationPermissionService
{
    private readonly ILogger _logger;

    public ApplicationPermissionService(
    ILogger<ApplicationPermissionService> logger
    )
    {
        _logger = logger;
    }

    public async Task<AuthorizationPolicyInfo> FetchPolicy(
           Guid policyId,
           CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _queries.Fetch(_tenantProvider.GetTenantId(), policyId).ConfigureAwait(false);
    }

    public async Task<AuthorizationPolicyInfo> FetchPolicy(
          string policyName,
          CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _queries.Fetch(_tenantProvider.GetTenantId(), policyName).ConfigureAwait(false);
    }


    public async Task<PolicyOperationResult> CreatePolicy(
           AuthorizationPolicyInfo policy,
           CancellationToken cancellationToken = default(CancellationToken))
    {
        if (policy == null) { throw new ArgumentException("policy cannot be null"); }
        policy.TenantId = _tenantProvider.GetTenantId();
        try
        {
            await _commands.Create(policy, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // if a policy is auto created it can happen that multipl request threads try to create the missing policy
            // a unique constraint error would happen so catching the possible error
            _log.LogError($"handled error {ex.Message}:{ex.StackTrace}");
        }


        return new PolicyOperationResult(true);
    }


}
