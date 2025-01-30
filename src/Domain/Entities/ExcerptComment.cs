
namespace SyriacSources.Backend.Domain.Entities;
public class ExcerptComment : BaseAuditableEntity
{
    public required int ContributorId {  get; set; }
    public ApplicationUser Contributor {  get; set; } = new ApplicationUser();
    public required string Details {  get; set; }
}

