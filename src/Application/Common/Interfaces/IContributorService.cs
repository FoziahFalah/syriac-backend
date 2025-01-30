using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface IContributorService
{
    Task<ApplicationUser?> GetContributorByEmailAsync(string emailAddress, CancellationToken cancellationToken);
    Task<ApplicationUser?> GetContributorByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result> CreateContributorAsync(ApplicationUser contributor, CancellationToken cancellationToken);
    Task<Result> DeleteContributorAsync(ApplicationUser contributor, CancellationToken cancellationToken);
    Task<Result> UpdateContributorAsync(ApplicationUser contributor, CancellationToken cancellationToken);
    Task<Result> DeactivateContributorAsync(ApplicationUser contributor, CancellationToken cancellationToken);
}
