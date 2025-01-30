
namespace SyriacSources.Backend.Domain.Entities;
public class Footnote : BaseAuditableEntity
{
    public int CommenterId { get; set; }
    public ApplicationUser Commenter { get; set; } = new ApplicationUser();
    public required string Comment { get; set; }

}
