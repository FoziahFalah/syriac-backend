
namespace SyriacSources.Backend.Domain.Entities;
public class Source : BaseAuditableEntity
{
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    public List<SourceDate> SourceDates { get; set; } = new();
    public int CenturyId { get; set; }
    public Century Century { get; set; } = null!;
    public string? Introduction { get; set; }
    public string? SourceTitleInArabic { get; set; }
    public string? SourceTitleInSyriac { get; set; }
    public string? SourceTitleInForeignLanguage { get; set; }
    public List<SourceIntroEditor> SourceIntroductionEditors { get; set; } = new();
    public ApplicationUser ? IntroductionEditor { get; set; } 
    public int? IntroductionEditorId { get; set; }
    public List<Publication> Publications { get; set; } = new();
    public CoverPhoto? CoverPhoto { get; set; }
    public List<Attachment> OtherAttachments { get; set; } = new();
    public string? AdditionalInfo { get; set; }


    public void AddCoverPhoto(CoverPhoto? cover)
    {
        if (cover == null) return;
        CoverPhoto = cover;
    }
    public void AddAttachments(IEnumerable<Attachment>? attachments)
    {
        if (attachments == null) return;
        OtherAttachments.AddRange(attachments);
    }
    public void AddPublications(IEnumerable<Publication>? publications)
    {
        if (publications == null) return;
        Publications.AddRange(publications);
    }
    public void AddSourceDates(IEnumerable<SourceDate>? dates)
    {
        if (dates == null) return;
        SourceDates.AddRange(dates);
    }


}
