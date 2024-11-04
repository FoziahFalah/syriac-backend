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
    //Task<Result> CreatePolicy(AuthorizationPolicyInfo policy, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result> CreatePolicy(string policy, CancellationToken cancellationToken = default(CancellationToken));
    Task<ApplicationPermission?> FetchPolicyById(Guid policyId, CancellationToken cancellationToken = default(CancellationToken));
    Task<List<string>?> FetchPermissionsByRoleIdAsync(int roleId, CancellationToken cancellationToken = default(CancellationToken));
    Task<ApplicationPermission?> FetchPolicyByName(string policyName, CancellationToken cancellationToken = default(CancellationToken));
}
