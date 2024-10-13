
namespace SyriacSources.Backend.Domain.Entities;
public class RolePermission : BaseAuditableEntity
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
    public Permission? Permission{ get; set; }
}
