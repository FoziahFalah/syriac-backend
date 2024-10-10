
namespace SyriacSources.Backend.Application.RolePermissions.Queries.GetRolePermissions;

public class RoleDto
{
    public int Id { get; init; }
    public string? DisplayName { get; init; }

    private class Mapping : Profile {}
}
