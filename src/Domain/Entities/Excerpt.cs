
namespace SyriacSources.Backend.Domain.Entities;
public class Excerpt : BaseAuditableEntity
{
    public int SourceId { get; set; }
    public int CoverPhotoId { get; set; }
    public Attachment CoverPhoto { get; set; } = new Attachment();
    public List<Attachment> OtherAttachments{ get; set; } = new List<Attachment>();
    public string? AdditionalInfo{ get; set; }


}
