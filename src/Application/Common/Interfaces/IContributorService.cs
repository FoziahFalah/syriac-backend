using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface IApplicationUserService
{
    Task<ApplicationUser?> GetApplicationUserByEmailAsync(string emailAddress, CancellationToken cancellationToken);
    Task<ApplicationUser?> GetApplicationUserByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result> CreateApplicationUserAsync(ApplicationUser contributor, CancellationToken cancellationToken);
    Task<Result> DeleteApplicationUserAsync(ApplicationUser contributor, CancellationToken cancellationToken);
    Task<Result> UpdateApplicationUserAsync(ApplicationUser contributor, CancellationToken cancellationToken);
    Task<Result> DeactivateApplicationUserAsync(ApplicationUser contributor, CancellationToken cancellationToken);
}
