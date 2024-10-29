
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Infrastructure.Data;

namespace SyriacSources.Backend.Infrastructure.Services;
public class ApplicationUserRoleService : IApplicationUserRoleService
{
    public readonly ApplicationDbContext _context;

    public ApplicationUserRoleService(ApplicationDbContext context)
    {
        _context = context; 
    }

    public async Task<Result> AddToRolesAsync(int userId, List<int> roles, CancellationToken cancellationToken)
    {
        ApplicationUserRole? result = await _context.ApplicationUserRoles.SingleOrDefaultAsync(x => x.ApplicationUserId == userId);

        if (result == null)
        {
            result = new ApplicationUserRole()
            {
                ApplicationUserId = userId,
            };
        }
        result.UserRoles = string.Join("|", roles);
        await _context.ApplicationUserRoles.AddAsync(result);
        return await _context.SaveChangesAsync(cancellationToken).ContinueWith(x => Result.Success()); 
    }

    public async Task<bool> IsInRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
    {
        ApplicationUserRole? userRoles = await _context.ApplicationUserRoles.Where(r => r.ApplicationUserId == userId).SingleOrDefaultAsync();
        if (userRoles == null )
        {
            return false;
        }
        string[] roles = userRoles.UserRoles.Split("|");
        
        return roles.Any(x => x == roleId.ToString());
    }
}
