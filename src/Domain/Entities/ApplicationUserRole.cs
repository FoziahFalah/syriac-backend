namespace SyriacSources.Backend.Domain.Entities;
public class ApplicationUserRole : BaseAuditableEntity
{
    public int ApplicationRoleId {  get; set; }
    public ApplicationRole? ApplicationRole {  get; set; }
    public int ContributorId { get; set; }
    public Contributor? Contributor { get; set; }
}
