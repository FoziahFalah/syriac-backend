
namespace SyriacSources.Backend.Domain.Entities;
public class ExcerptComment : BaseAuditableEntity
{
    public required int ApplicationUserId {  get; set; }
    public ApplicationUser ApplicationUser {  get; set; } = null!;
    public required string Details {  get; set; }
}

