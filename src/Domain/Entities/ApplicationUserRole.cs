namespace SyriacSources.Backend.Domain.Entities;
public class ApplicationUserRole : BaseAuditableEntity
{
    public string UserRoles { get; set; } = "";
    public int UserId { get; set; }
}
