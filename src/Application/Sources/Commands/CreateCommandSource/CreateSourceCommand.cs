using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.Sources.Commands.CreateSource;
public class CreateSource : IRequest<int>
{
    public int AuthorId { get; set; }
    public int CenturyId { get; set; }
    public int? IntroductionEditorId { get; set; }
    public List<SourceDateDto>? SourceDates { get; set; }
    public string? Introduction { get; set; }
    public string? SourceTitleInArabic { get; set; }
    public string? SourceTitleInSyriac { get; set; }
    public string? SourceTitleInForeignLanguage { get; set; }
    public string? AdditionalInfo { get; set; }
    public CoverPhotoDto? CoverPhoto { get; set; }
    public List<AttachmentDto>? OtherAttachments { get; set; }
    public List<PublicationDto>? Publications { get; set; }
}

public class CreateSourceHandler : IRequestHandler<CreateSource, int>
{
    private readonly IApplicationDbContext _context;
    public CreateSourceHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateSource request, CancellationToken cancellationToken)
    {


        ApplicationUser? editor = null;
        if (request.IntroductionEditorId.HasValue)
        {
            editor = await _context.ApplicationUsers.FindAsync(new object[] { request.IntroductionEditorId.Value }, cancellationToken);
            Guard.Against.NotFound(request.IntroductionEditorId.Value, editor);
        }
        var entity = new Source
        {
            AuthorId = request.AuthorId,
            CenturyId = request.CenturyId,
            Introduction = request.Introduction,
            SourceTitleInArabic = request.SourceTitleInArabic,
            SourceTitleInSyriac = request.SourceTitleInSyriac,
            SourceTitleInForeignLanguage = request.SourceTitleInForeignLanguage,
            AdditionalInfo = request.AdditionalInfo,
            IntroductionEditorId = editor?.Id,
            Publications = new(),
            OtherAttachments = new()
        };
        var coverEntity = request.CoverPhoto != null
         ? new CoverPhoto
         {
             FileName = request.CoverPhoto.FileName,
             FilePath = request.CoverPhoto.FilePath,
             FileExtension = request.CoverPhoto.FileExtension
         }
         : null;
        // تحويل المرفقات
        var attachmentsEntity = request.OtherAttachments?.Select(a => new Attachment
        {
            FileName = a.FileName,
            FilePath = a.FilePath,
            FileExtension = a.FileExtension
        }).ToList();
        // تحويل النشرات
        var publicationsEntity = request.Publications?.Select(p => new Publication
        {
            Url = p.Url,
            Description = p.Description
        }).ToList();
        var sourceDatesEntity = request.SourceDates?
    .Select(d => new SourceDate
    {
        DateFormatId = d.DateFormatId,
        FromYear = d.FromYear,
        ToYear = d.ToYear
    }).ToList();
        // استدعاء الدوال داخل كلاس Source.cs
        entity.AddCoverPhoto(coverEntity);
        entity.AddAttachments(attachmentsEntity);
        entity.AddPublications(publicationsEntity);
        entity.AddSourceDates(sourceDatesEntity);
        _context.Sources.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
