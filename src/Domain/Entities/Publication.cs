
namespace SyriacSources.Backend.Domain.Entities;
public class SourcePublication : BaseAuditableEntity
{
    public int SourceId {  get; set; }
    public string? Description {  get; set; }
    public required string Url {  get; set; }
}

