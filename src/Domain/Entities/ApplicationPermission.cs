using System.ComponentModel.DataAnnotations;
namespace SyriacSources.Backend.Domain.Entities;
public class ApplicationPermission : BaseAuditableEntity
{
    public required string NormalizedPermissionName { get; set; }
    public required string NameEN {  get; set; }
    public required string NameAR {  get; set; }
    public int ParentId { get; set; } = 0;
    public bool IsModule { get; set; } = false;
    public string? Description { get; set; }
}
