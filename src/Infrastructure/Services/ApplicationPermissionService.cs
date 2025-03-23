
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SyriacSources.Backend.Application.Common.Constants;
using SyriacSources.Backend.Application.Common.Extensions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Constants;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Services;
public class ApplicationPermissionService : IApplicationPermissionService
{
    private readonly ILogger _logger;
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _roleService;

    public ApplicationPermissionService(
         IApplicationDbContext context,
         IApplicationRoleService roleService,
         ILogger<ApplicationPermissionService> logger
    )
    {
        _context = context;
        _logger = logger;
        _roleService = roleService;
    }

    public async Task<ApplicationPermission?> FetchPolicyById( Guid policyId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _context.ApplicationPermissions.FindAsync(policyId);
    }

    public async Task<List<string?>> FetchPoliciesByRoleIdAsync(int roleId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // Ensure roles is not empty before querying

        List<ApplicationRolePermission> permissions = await _context.ApplicationRolePermissions.Where(x => x.ApplicationRoleId == roleId)
            .Include(x => x.ApplicationPermission)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken); ;

        List<string?> policies = permissions.Select(x => x.ApplicationPermission?.PolicyName).ToList();

        return policies;
    }

    public async Task<List<string?>> FetchPoliciesByRolesAsync(List<int> roles, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // Ensure roles is not empty before querying
        if (roles == null || !roles.Any())
        {
            return new List<string?>(); // Or handle as needed
        }

        List<ApplicationRolePermission> permissions = await _context.ApplicationRolePermissions.Where(x => roles.Contains(x.ApplicationRoleId) && x.IsActive)
                    .Include(x=>x.ApplicationPermission)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
        
        List<string?> policies = permissions.Select(x => x.ApplicationPermission?.PolicyName).ToList();

        return policies;
    }

    public async Task<List<string>> FetchPoliciesAsync(CancellationToken cancellationToken)
    {
        return await _context.ApplicationPermissions.Where(x => x.IsActive).Select(x=>x.PolicyName).ToListAsync(cancellationToken) ?? new List<string>();
    }

    public async Task<bool> PolicyExistsAsync(string policy, CancellationToken cancellationToken)
    {
        return await _context.ApplicationPermissions.AnyAsync(x => x.PolicyName ==  policy);
    }

    public async Task<ApplicationPermission?> FetchPolicyByName( string policyName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _context.ApplicationPermissions.Where(x => x.PolicyName == policyName).FirstOrDefaultAsync();
    }

    public async Task CreatePolicy( string policy, CancellationToken cancellationToken)
    {
        try
        {
            var permissionEntity = await _context.ApplicationPermissions.Where(x => x.PolicyName == policy).SingleOrDefaultAsync();
            int parentId = 0;

            if (permissionEntity != null)
            {
                return;
            }

            if(String.IsNullOrEmpty(policy))
            {
                throw new ArgumentException("policy cannot be null");
            }

            var splitPolicy = policy.Split(TextDelimitersConstants.PolicyDelimiter);

            var parentName = splitPolicy[0];

            var parentPermissionEntity = await _context.ApplicationPermissions.FirstOrDefaultAsync(c => c.PolicyName == parentName);

            //adding parent Policy
            if (parentPermissionEntity == null)
            {
                parentPermissionEntity = new ApplicationPermission()
                {
                    NameAR = parentName,
                    NameEN = parentName,
                    PolicyName = parentName,
                    IsModule = true,
                    Created = DateTime.Now,
                    CreatedBy = "auto generated"
                };
                _context.ApplicationPermissions.Add(parentPermissionEntity);
                parentId = await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                parentId = parentPermissionEntity.Id;
            }

            //adding parentId to new sub-policy
            if (!String.IsNullOrEmpty(splitPolicy[1]))
            {
                permissionEntity = new ApplicationPermission
                {
                    NameEN = splitPolicy[1],
                    NameAR = splitPolicy[1],
                    PolicyName = policy,
                    ParentId = parentId,
                    Created = DateTime.Now,
                    CreatedBy = "auto generated"
                };
                _context.ApplicationPermissions.Add(permissionEntity);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"handled error {ex.Message}:{ex.StackTrace}");
        }
    }
    public async Task CreatePoliciesBatch(List<string> policies, CancellationToken cancellationToken)
    {
        var newPolicies = policies.Select(policy => new ApplicationPermission
        {
            NameEN = policy,
            NameAR = policy,
            PolicyName = policy,
            Created = DateTime.Now,
            CreatedBy = "auto generated"
        }).ToList();

        _context.ApplicationPermissions.AddRange(newPolicies); // Batch insert
        await _context.SaveChangesAsync(cancellationToken); // Commit once
    }

}
