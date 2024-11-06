
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

    public async Task<ApplicationPermission?> FetchPolicyById(
           Guid policyId,
           CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _context.ApplicationPermissions.FindAsync(policyId);
    }

    public async Task<List<string>?> FetchPermissionsByRoleIdAsync(
           int roleId,
           CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        var rolePermission = await _context.ApplicationRolePermissions.Where(x => x.ApplicationRoleId == roleId).SingleOrDefaultAsync();

        if(rolePermission == null || rolePermission.ApplicationPermissionIds == null) {
            return null;
        }

        List<string> permissionIds = rolePermission.ApplicationPermissionIds.Split(',').ToList();

        List<string>? policies = await _context.ApplicationPermissions.Where(x => permissionIds.Any(p => p.Trim() == x.Id.ToString())).Select(n => n.PolicyName).ToListAsync();

        return policies;
    }

    public async Task<ApplicationPermission?> FetchPolicyByName(
          string policyName,
          CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _context.ApplicationPermissions.Where(x => x.PolicyName == policyName).FirstOrDefaultAsync();
    }

    public async Task<Result> CreatePolicy(
        string policy,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        //if (policy == null) { throw new ArgumentException("policy cannot be null"); }

        try
        {
            var permissionEntity = await _context.ApplicationPermissions.Where(x => x.PolicyName == policy).SingleOrDefaultAsync();

            if (permissionEntity != null)
            {
                return Result.Success();
            }

            if(String.IsNullOrEmpty(policy))
            {
                throw new ArgumentException("policy cannot be null");
            }

            var parentName = policy.Split(":")[0];
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

            //adding parentId to new Policy
            permissionEntity = new ApplicationPermission
            {
                NameEN = policy,
                NameAR = policy,
                PolicyName = policy,
                ParentId = parentPermissionEntity == null ? 0 : parentPermissionEntity.Id,
                Created = DateTime.Now,
                CreatedBy = "auto generated"
        };
            _context.ApplicationPermissions.Add(permissionEntity);
            //Map Role to Permission
            var result = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"handled error {ex.Message}:{ex.StackTrace}");
        }


        return Result.Success();
    }

}
