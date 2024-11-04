
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

    //public async Task<Result> CreatePolicy(
    //    AuthorizationPolicyInfo policy,
    //    CancellationToken cancellationToken = default(CancellationToken))
    //{
    //    if (policy == null) { throw new ArgumentException("policy cannot be null"); }
        
    //    try
    //    {
    //        var permissionEntity = await _context.ApplicationPermissions.Where(x => x.PolicyName == policy.Name).SingleOrDefaultAsync();

    //        if(permissionEntity != null) {
    //            return Result.Success();
    //        }

    //        IEnumerable<ApplicationRole> roles = (await _roleService.GetRolesAsync(cancellationToken)).Where(x => (policy.AllowedRoles.Any(r => r.NormalizeString() == x.NormalizedRoleName)));

    //        var parentName = policy?.Name?.Split(":")[0];
    //        var parentPermissionEntity = await _context.ApplicationPermissions.Where(c => c.PolicyName == parentName).FirstOrDefaultAsync();

    //        //adding parent Policy
    //        if(parentPermissionEntity == null && !String.IsNullOrEmpty(parentName)) 
    //        {
    //            parentPermissionEntity = new ApplicationPermission()
    //            {
    //                NameAR = parentName,
    //                NameEN = parentName,
    //                PolicyName = policy?.Name,
    //                IsModule = true
    //            };
    //        }

    //        //adding parentId to new Policy
    //        permissionEntity = new ApplicationPermission
    //        {
    //            NameEN = policy?.Name,
    //            NameAR = policy?.Name,
    //            PolicyName = policy?.Name,
    //            ParentId = parentPermissionEntity == null ? 0 : parentPermissionEntity.Id,

    //        };

    //        //Map Role to Permission
    //        var result = await _context.SaveChangesAsync(cancellationToken);


    //        //New Permissions are added to the default Roles
    //        if (result > 0)
    //        {
    //            var rolePermissions = await _context.ApplicationRolePermissions.Where(x => roles.Any(r => r.Id == x.ApplicationRoleId)).ToListAsync(); // get roles identified in the AllowedRoles

    //            foreach (var role in roles)
    //            {
    //                var rolePermission = rolePermissions?.Where(x => x.ApplicationRoleId == role.Id).SingleOrDefault();
    //                string appendedPermissions = "";
    //                if (rolePermission != null)
    //                {
    //                    appendedPermissions = rolePermission.ApplicationPermissionIds + "," + permissionEntity.Id;
    //                }
    //                await _roleService.UpdateRolePermissions(role.Id, appendedPermissions, cancellationToken);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError($"handled error {ex.Message}:{ex.StackTrace}");
    //    }


    //    return Result.Success();
    //}


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
                    PolicyName = policy,
                    IsModule = true
                };
            }

            //adding parentId to new Policy
            permissionEntity = new ApplicationPermission
            {
                NameEN = policy,
                NameAR = policy,
                PolicyName = policy,
                ParentId = parentPermissionEntity == null ? 0 : parentPermissionEntity.Id,

            };

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
