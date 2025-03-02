
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Infrastructure.Data;

namespace SyriacSources.Backend.Infrastructure.Services;
public class ApplicationUserRoleService : IApplicationUserRoleService
{
    public readonly ApplicationDbContext _context;
    public readonly JsonDelimiters _jsonDelimiters;

    public ApplicationUserRoleService(ApplicationDbContext context,IOptions<JsonDelimiters> jsonDelimiters)
    {
        _context = context;
        _jsonDelimiters = jsonDelimiters.Value;
    }

    public async Task<Result> AddToRolesAsync(int userId, List<int> roles, CancellationToken cancellationToken)
    {
        ApplicationUserRole? result = await _context.ApplicationUserRoles.SingleOrDefaultAsync(x => x.UserId == userId);

        if (result == null)
        {
            result = new ApplicationUserRole()
            {
                UserId = userId,
            };
        }
        result.UserRoles = string.Join("|", roles);
        await _context.ApplicationUserRoles.AddAsync(result);
        return await _context.SaveChangesAsync(cancellationToken).ContinueWith(x => Result.Success(null)); 
    }

    public async Task<bool> IsInRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
    {
        ApplicationUserRole? userRoles = await _context.ApplicationUserRoles.Where(r => r.UserId == userId).SingleOrDefaultAsync();
        if (userRoles == null )
        {
            return false;
        }
        string[] roles = userRoles.UserRoles.Split(_jsonDelimiters.CSVDelimiter);
        
        return roles.Any(x => x == roleId.ToString());
    }
}
