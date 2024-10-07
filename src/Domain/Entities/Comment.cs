
namespace SyriacSources.Backend.Domain.Entities;
public class Comment : BaseAuditableEntity
{
    public required int CommenterId {  get; set; }
    public Contributor Commenter {  get; set; } = new Contributor();
    public required string Details {  get; set; }
}

