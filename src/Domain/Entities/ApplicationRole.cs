namespace SyriacSources.Backend.Domain.Entities;
public class ApplicationRole : BaseAuditableEntity
{
    public required string NormalizedRoleName { get; set; }
    public required string NameEN {  get; set; }
    public required string NameAR {  get; set; }
}
