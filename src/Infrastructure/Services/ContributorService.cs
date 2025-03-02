using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Application.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace SyriacSources.Backend.Infrastructure.Services;
public class ApplicationUserService : IApplicationUserService
{

    private readonly IApplicationDbContext _context;
    private readonly ILogger<ApplicationUserService> _logger;

    public ApplicationUserService(IApplicationDbContext context, ILogger<ApplicationUserService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApplicationUser?> GetApplicationUserByEmailAsync(string emailAddress, CancellationToken cancellationToken)
    {
        ApplicationUser? entity = await _context.ApplicationUsers.SingleOrDefaultAsync(x => x.Email == emailAddress);
        return entity;
    }
    public async Task<ApplicationUser?> GetApplicationUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        ApplicationUser? entity = await _context.ApplicationUsers.SingleOrDefaultAsync(x => x.Id == id);
        return entity;
    }

    public async Task<Result> CreateApplicationUserAsync(ApplicationUser contributor, CancellationToken cancellationToken)
    {
        int result = 0;

        try{
            _context.ApplicationUsers.Add(contributor);
            result = await _context.SaveChangesAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ex.Message);
        }

        return result.ToApplicationResult();
    }


    public async Task<Result> UpdateApplicationUserAsync(ApplicationUser contributor, CancellationToken cancellationToken)
    {
        int result = 0;
        try
        {
            _context.ApplicationUsers.Update(contributor);
            result = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return result.ToApplicationResult();
    }

    public async Task<Result> DeleteApplicationUserAsync(ApplicationUser contributor, CancellationToken cancellationToken)
    {
        int result = 0;
        try
        {
            _context.ApplicationUsers.Remove(contributor);
            result = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return result.ToApplicationResult();
    }


    public async Task<Result> DeactivateApplicationUserAsync(ApplicationUser contributor, CancellationToken cancellationToken)
    {
        int result = 0;
        try
        {
            
            result = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return result.ToApplicationResult();
    }
}
