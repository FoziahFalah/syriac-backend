namespace SyriacSources.Backend.Domain.Entities;
public class ApplicationRolePermission: BaseAuditableEntity
{
    public int ApplicationRoleId { get; set; }
    public ApplicationRole? ApplicationRole { get; init; }
    public int ApplicationPermissionId { get; set; } 
    public ApplicationPermission ApplicationPermission  { get; set; } = new ApplicationPermission();
}
