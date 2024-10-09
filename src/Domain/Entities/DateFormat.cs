
namespace SyriacSources.Backend.Domain.Entities;
public class DateFormat : BaseAuditableEntity
{
    public required string Format { get; set; }
    public required string Period { get; set; }
}
