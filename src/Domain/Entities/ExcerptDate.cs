
namespace SyriacSources.Backend.Domain.Entities;
public class ExcerptDate : BaseAuditableEntity
{
    public int ExcerptId { get; set; }
    public int DateFormatId { get; set; }
    public DateFormat DateFormat { get; set; } = null!;
    public int FromYear { get; set; }
    public int ToYear { get; set; }
}






