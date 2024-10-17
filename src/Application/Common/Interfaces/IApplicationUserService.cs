using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.Users;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface IApplicationUserService
{
    Task<(Result,int)> CreateUser(Contributor contributor, CancellationToken cancellationToken);
    Task<(Result,int)> DeleteUser(Contributor contributor, CancellationToken cancellationToken);
    Task<(Result,int)> UpdateUser(Contributor contributor, CancellationToken cancellationToken);
}
