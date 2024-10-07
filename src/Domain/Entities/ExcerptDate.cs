
namespace SyriacSources.Backend.Domain.Entities;
public class ExcerptDate : BaseAuditableEntity
{
    public int ExcerptId { get; set; }
    public int DateFormatId { get; set; }
    public int FromYear { get; set; }
    public int ToYear { get; set; }
}
