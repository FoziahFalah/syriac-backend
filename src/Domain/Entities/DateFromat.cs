
namespace SyriacSources.Backend.Domain.Entities;
public class DateFromat : BaseAuditableEntity
{
    public required string DateFormat { get; set; }
    public required string Period { get; set; }
}
