
namespace SyriacSources.Backend.Domain.Entities;
public class ExcerptComment : BaseAuditableEntity
{
    public required int ApplicationUserId {  get; set; }
    public ApplicationUser ApplicationUser {  get; set; } = new ApplicationUser();
    public required string Details {  get; set; }
}

