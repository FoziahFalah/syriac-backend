namespace SyriacSources.Backend.Domain.Entities;
public class ApplicationRolePermission: BaseAuditableEntity
{
    public int ApplicationRoleId { get; set; }
    public ApplicationRole? ApplicationRole { get; init; }
    public string? ApplicationPermissionIds { get; set; } 
}
