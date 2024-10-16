
namespace SyriacSources.Backend.Domain.Entities;
public class Excerpt : BaseAuditableEntity
{
    public int SourceId { get; set; }
    public Source Source { get; set; } = new Source();
    public string? AdditionalInfo{ get; set; }
}
