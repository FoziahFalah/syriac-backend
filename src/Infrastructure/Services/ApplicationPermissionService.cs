using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Services;
public class ApplicationPermissionService
{
    private readonly ILogger _logger;
    private readonly IApplicationDbContext _context;

    public ApplicationPermissionService(
         IApplicationDbContext context,
        ILogger<ApplicationPermissionService> logger
    )
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApplicationPermission> FetchPolicyById(
           Guid policyId,
           CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _queries.Fetch(_tenantProvider.GetTenantId(), policyId).ConfigureAwait(false);
    }

    public async Task<ApplicationPermission> FetchPolicyByName(
          string policyName,
          CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _queries.Fetch(_tenantProvider.GetTenantId(), policyName).ConfigureAwait(false);
    }


    public async Task<Result> CreatePolicy(
           string policy,
           CancellationToken cancellationToken = default(CancellationToken))
    {
        if (policy == null) { throw new ArgumentException("policy cannot be null"); }
        
        try
        {
            var entity = await _context.ApplicationPermissions.Where(x => x.PolicyName == policy).SingleOrDefaultAsync();

            if(entity != null) {
                return;
            }
            var parentName = entity.PolicyName?.Split(":")[0];
            var parent = await _context.ApplicationPermissions.Where(c => c.PolicyName == parentName).FirstOrDefaultAsync();
            if(parent == null) 
            {
                parent = new ApplicationPermission()
                {
                    NameAR = parentName,
                    NameEN = parentName,
                    PolicyName = policy,
                    IsModule = true
                };
            }
            entity = new ApplicationPermission
            {
                NameEN = policy,
                NameAR = policy,
                PolicyName=policy,
                ParentId = entity.Id,
                
            },
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            // if a policy is auto created it can happen that multipl request threads try to create the missing policy
            // a unique constraint error would happen so catching the possible error
            _logger.LogError($"handled error {ex.Message}:{ex.StackTrace}");
        }


        return new Result(true,null);
    }


}
