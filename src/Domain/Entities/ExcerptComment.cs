
namespace SyriacSources.Backend.Domain.Entities;
public class ExcerptComment : BaseAuditableEntity
{
    public required int ContributorId {  get; set; }
    public Contributor Contributor {  get; set; } = new Contributor();
    public required string Details {  get; set; }
}

