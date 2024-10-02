
namespace SyriacSources.Backend.Domain.Entities;
public class Source : BaseAuditableEntity
{
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    public DateTime DocumentedOnHijri{ get; set; }
    public DateTime DocumentedOnGregorian{ get; set; }
    public int CenturyId{ get; set; }
    public Century Century { get; set; } = null!;
    public string? Introduction { get; set; }
    public string? SourceTitleInArabic { get; set; }
    public string? SourceTitleInSyriac { get; set; }
    public string? SourceTitleInForeignLanguage { get; set; }
    public List<SourceInroductionEditor> SourceIntroductionEditors { get; set; } = new List<SourceInroductionEditor>();
    public ApplicationUser IntroductionEditor { get; set; }
    public List<SourcePublication> Publications { get; set; } = new List<SourcePublication> { };
    public int CoverPhotoId { get; set; }
    public Attachment CoverPhoto { get; set; } = new Attachment();
    public List<Attachment> OtherAttachments{ get; set; } = new List<Attachment> { };
    public string? AdditionalInfo{ get; set; }


}
