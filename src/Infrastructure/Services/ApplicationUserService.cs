using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Application.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace SyriacSources.Backend.Infrastructure.Services;
public class ApplicationUserService : IApplicationUserService
{

    private readonly IApplicationDbContext _context;
    private readonly IIdentityApplicationUserService _identityUserService;
    private readonly ILogger<ApplicationUserService> _logger;

    public ApplicationUserService(IApplicationDbContext context, ILogger<ApplicationUserService> logger, IIdentityApplicationUserService identityUserService)
    {
        _context = context;
        _logger = logger;
        _identityUserService = identityUserService;
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

    public async Task<ApplicationUser?> GetApplicationUserByUserNameAsync(string username, CancellationToken cancellationToken)
    {
        ApplicationUser? entity = await _context.ApplicationUsers.SingleOrDefaultAsync(x => x.UserName == username);
        return entity;
    }

    public async Task<Result> CreateApplicationUserAsync(ApplicationUser appUser, string password, CancellationToken cancellationToken)
    {
        int result = 0;
        
        try{
            var identityId = await _identityUserService.CreateUserLoginAsync(appUser,password);

            if( identityId <= 0)
            {
                return Result.Failure("Error on Creating User");
            }

            appUser.IdentityApplicationUserId = identityId;

            _context.ApplicationUsers.Add(appUser);

            result = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ex.Message);
        }

        return result.ToApplicationResult();
    }


    public async Task<Result> UpdateApplicationUserAsync(ApplicationUser appUser, CancellationToken cancellationToken)
    {
        int result = 0;
        try
        {
            _context.ApplicationUsers.Update(appUser);
            result = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return result.ToApplicationResult();
    }

    public async Task<Result> DeleteApplicationUserAsync(ApplicationUser appUser, CancellationToken cancellationToken)
    {
        int result = 0;
        try
        {
            _context.ApplicationUsers.Remove(appUser);
            result = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return result.ToApplicationResult();
    }


    public async Task<Result> DeactivateApplicationUserAsync(ApplicationUser appUser, CancellationToken cancellationToken)
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
