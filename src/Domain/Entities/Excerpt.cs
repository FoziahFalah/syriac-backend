
namespace SyriacSources.Backend.Domain.Entities;
public class Excerpt : BaseAuditableEntity
{
    public int SourceId { get; set; }
    public Source Source { get; set; } = null!;
    public string? AdditionalInfo{ get; set; }
    public List<ExcerptText> ExcerptTexts { get; set; } = null!;
    public List<ExcerptDate> ExcerptDates { get; set; } = new();
    public List<ExcerptComment> ExcerptComments { get; set; } = null!;
}
