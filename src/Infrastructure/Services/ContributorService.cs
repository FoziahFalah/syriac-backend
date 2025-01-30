using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Application.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace SyriacSources.Backend.Infrastructure.Services;
public class ContributorService : IContributorService
{

    private readonly IApplicationDbContext _context;
    private readonly ILogger<ContributorService> _logger;

    public ContributorService(IApplicationDbContext context, ILogger<ContributorService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApplicationUser?> GetContributorByEmailAsync(string emailAddress, CancellationToken cancellationToken)
    {
        ApplicationUser? entity = await _context.Contributors.SingleOrDefaultAsync(x => x.EmailAddress == emailAddress);
        return entity;
    }
    public async Task<ApplicationUser?> GetContributorByIdAsync(int id, CancellationToken cancellationToken)
    {
        ApplicationUser? entity = await _context.Contributors.SingleOrDefaultAsync(x => x.Id == id);
        return entity;
    }

    public async Task<Result> CreateContributorAsync(ApplicationUser contributor, CancellationToken cancellationToken)
    {
        int result = 0;

        try{
            _context.Contributors.Add(contributor);
            result = await _context.SaveChangesAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ex.Message);
        }

        return result.ToApplicationResult();
    }


    public async Task<Result> UpdateContributorAsync(ApplicationUser contributor, CancellationToken cancellationToken)
    {
        int result = 0;
        try
        {
            _context.Contributors.Update(contributor);
            result = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return result.ToApplicationResult();
    }

    public async Task<Result> DeleteContributorAsync(ApplicationUser contributor, CancellationToken cancellationToken)
    {
        int result = 0;
        try
        {
            _context.Contributors.Remove(contributor);
            result = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return result.ToApplicationResult();
    }


    public async Task<Result> DeactivateContributorAsync(ApplicationUser contributor, CancellationToken cancellationToken)
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
