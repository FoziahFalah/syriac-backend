
namespace SyriacSources.Backend.Domain.Entities;
public class Footnote : BaseAuditableEntity
{
    public int CommenterId { get; set; }
    public Contributor Commenter { get; set; } = new Contributor();
    public required string Comment { get; set; }

}
