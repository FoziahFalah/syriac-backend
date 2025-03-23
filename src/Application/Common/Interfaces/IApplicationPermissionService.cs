using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Common.Interfaces;
public interface IApplicationPermissionService
{
    Task<List<string>> FetchPoliciesAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task CreatePolicy(string policy, CancellationToken cancellationToken = default(CancellationToken));
    Task CreatePoliciesBatch(List<string> policy, CancellationToken cancellationToken = default(CancellationToken));
    Task<ApplicationPermission?> FetchPolicyById(Guid policyId, CancellationToken cancellationToken = default(CancellationToken));
    Task<List<string?>> FetchPoliciesByRoleIdAsync(int roleId, CancellationToken cancellationToken);
    Task<List<string?>> FetchPoliciesByRolesAsync(List<int> roles, CancellationToken cancellationToken);
    Task<ApplicationPermission?> FetchPolicyByName(string policyName, CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> PolicyExistsAsync(string policyName, CancellationToken cancellationToken = default(CancellationToken));
}
