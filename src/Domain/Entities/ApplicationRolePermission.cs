namespace SyriacSources.Backend.Domain.Entities;
public class ApplicationRolePermission: BaseAuditableEntity
{
    public int ApplicationRoleId { get; set; }
    public ApplicationRole? ApplicationRole { get; init; }
    public int ContributorId { get; set; } 
    public Contributor Contributor { get; set; } = new Contributor();
}
