
namespace SyriacSources.Backend.Domain.Entities;
public class Comment : BaseAuditableEntity
{
    public required int CommenterId {  get; set; }
    public ApplicationUser Commenter {  get; set; }
    public required string Details {  get; set; }
}

