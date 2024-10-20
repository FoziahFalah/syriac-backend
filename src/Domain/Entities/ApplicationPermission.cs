using System.ComponentModel.DataAnnotations;
namespace SyriacSources.Backend.Domain.Entities;
public class ApplicationPermission : BaseAuditableEntity
{
    public string? NormalizedPermissionName { get; set; }
    public string? NameEN {  get; set; }
    public string? NameAR {  get; set; }
    public int ParentId { get; set; } = 0;
    public bool IsModule { get; set; } = false;
    public string? Description { get; set; }
}
