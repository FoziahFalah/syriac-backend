using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Permissions.Queries.GetPermissions;

public class PermissionDto
{
    public int Id { get; init; }
    public string? DisplayName { get; init; }

    private class Mapping : Profile {}
}
