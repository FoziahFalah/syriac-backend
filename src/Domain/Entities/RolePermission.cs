

using SyriacSources.Backend.Infrastructure.Identity;

namespace SyriacSources.Backend.Domain.Entities;
public class RolePermission : BaseAuditableEntity
{
    public int RoleId { get; set; }
    public Role? Role { get; set; }
    public string? PermissionId { get; set; }
}
