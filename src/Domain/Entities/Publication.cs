
namespace SyriacSources.Backend.Domain.Entities;
public class Publication : BaseAuditableEntity
{
    public int SourceId {  get; set; }
    public string? Description {  get; set; }
    public required string Url {  get; set; }
}

