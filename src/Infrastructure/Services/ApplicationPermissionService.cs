
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SyriacSources.Backend.Application.Common.Extensions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Services;
public class ApplicationPermissionService : IApplicationPermissionService
{
    private readonly ILogger _logger;
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _roleService;
    private readonly JsonDelimiters _jsonDelimiters;

    public ApplicationPermissionService(
         IApplicationDbContext context,
         IApplicationRoleService roleService,
         ILogger<ApplicationPermissionService> logger,
         IOptions<JsonDelimiters> jsonDelimiters
    )
    {
        _context = context;
        _logger = logger;
        _roleService = roleService;
        _jsonDelimiters = jsonDelimiters.Value;
    }

    public async Task<ApplicationPermission?> FetchPolicyById( Guid policyId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _context.ApplicationPermissions.FindAsync(policyId);
    }

    public async Task<List<string>?> FetchPoliciesByRoleIdAsync(int roleId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var rolePermission = await _context.ApplicationRolePermissions.Where(x => x.ApplicationRoleId == roleId && x.IsActive).SingleOrDefaultAsync();

        if (rolePermission == null || rolePermission.ApplicationPermissionIds == null)
        {
            return null;
        }

        List<string> permissionIds = rolePermission.ApplicationPermissionIds.Split(_jsonDelimiters.CSVDelimiter).ToList();

        List<string>? policies = await _context.ApplicationPermissions.Where(x => permissionIds.Any(p => p.Trim() == x.Id.ToString())).Select(n => n.PolicyName).ToListAsync();

        return policies;
    }

    public async Task<List<string>?> FetchPoliciesByRolesAsync(List<int> roles, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        // Ensure roles is not empty before querying
        if (roles == null || !roles.Any())
        {
            return null; // Or handle as needed
        }

        List<string?> permissionIds = await _context.ApplicationRolePermissions.Where(x => roles.Contains(x.ApplicationRoleId) && x.IsActive).Select(x => x.ApplicationPermissionIds).ToListAsync(cancellationToken);

        List<string>? policies = await _context.ApplicationPermissions.Where(x => permissionIds.Any(p => !string.IsNullOrEmpty(p) && p.Trim() == x.Id.ToString())).Select(n => n.PolicyName).ToListAsync();

        return policies;
    }

    public async Task<List<string>?> FetchPoliciesAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var poliicies = await _context.ApplicationPermissions.Where(x => x.IsActive).Select(x=>x.PolicyName).ToListAsync();

        if (poliicies == null )
        {
            return null;
        }

        //List<string>? policies = await _context.ApplicationPermissions.Where(x => permissionIds.Any(p => p.Trim() == x.Id.ToString())).Select(n => n.PolicyName).ToListAsync();

        return poliicies;
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

            if (permissionEntity != null)
            {
                return;
            }

            if(String.IsNullOrEmpty(policy))
            {
                throw new ArgumentException("policy cannot be null");
            }

            var parentName = policy.Split(_jsonDelimiters.PermissionDelimiter)[0];
            var parentPermissionEntity = await _context.ApplicationPermissions.Where(c => c.PolicyName == parentName).FirstOrDefaultAsync();

            //adding parent Policy
            if (parentPermissionEntity == null && !String.IsNullOrEmpty(parentName))
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
            }
            var result = await _context.SaveChangesAsync(cancellationToken);
            
            //adding parentId to new Policy
            permissionEntity = new ApplicationPermission
            {
                NameEN = policy,
                NameAR = policy,
                PolicyName = policy,
                ParentId = result,
                Created = DateTime.Now,
                CreatedBy = "auto generated"
        };
            _context.ApplicationPermissions.Add(permissionEntity);

            //Map Role to Permission
            result = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"handled error {ex.Message}:{ex.StackTrace}");
        }
    }
}
