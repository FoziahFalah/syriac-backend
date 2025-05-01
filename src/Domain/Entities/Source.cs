
namespace SyriacSources.Backend.Domain.Entities;
public class Source : BaseAuditableEntity
{
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    public DateTime DocumentedOnHijri { get; set; }
    public DateTime DocumentedOnGregorian { get; set; }
    public int CenturyId { get; set; }
    public Century Century { get; set; } = null!;
    public string? Introduction { get; set; }
    public string? SourceTitleInArabic { get; set; }
    public string? SourceTitleInSyriac { get; set; }
    public string? SourceTitleInForeignLanguage { get; set; }
    public List<SourceIntroEditor> SourceIntroductionEditors { get; set; } = new();
    public ApplicationUser IntroductionEditor { get; set; } = new();
    public List<Publication> Publications { get; set; } = new();
    public CoverPhoto? CoverPhoto { get; set; } // فقط هذا موجود
    public List<Attachment> OtherAttachments { get; set; } = new();
    public string? AdditionalInfo { get; set; }
    public int? IntroductionEditorId { get; set; }
}
