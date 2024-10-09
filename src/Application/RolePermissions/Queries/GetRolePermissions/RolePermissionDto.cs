
namespace SyriacSources.Backend.Application.RolePermissions.Queries.GetRolePermissions;

public class RolePermissionDto
{
    public int Id { get; init; }
    public string? DisplayName { get; init; }

    private class Mapping : Profile {}
}
