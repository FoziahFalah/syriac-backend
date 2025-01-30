
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
    public List<SourceIntroEditor> SourceIntroductionEditors { get; set; } = new List<SourceIntroEditor>();
    public ApplicationUser IntroductionEditor { get; set; } = new ApplicationUser();
    public List<Publication> Publications { get; set; } = new List<Publication> { };
    public int CoverPhotoId { get; set; }
    public Attachment CoverPhoto { get; set; } = new Attachment();
    public List<Attachment> OtherAttachments{ get; set; } = new List<Attachment> { };
    public string? AdditionalInfo{ get; set; }


}
