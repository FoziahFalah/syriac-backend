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
        var author = await _context.Authors.FindAsync(new object[] { request.AuthorId }, cancellationToken);
        var century = await _context.Centuries.FindAsync(new object[] { request.CenturyId }, cancellationToken);
        Guard.Against.NotFound(request.AuthorId, author);
        Guard.Against.NotFound(request.CenturyId, century);
        ApplicationUser? editor = null;
        if (request.IntroductionEditorId.HasValue)
        {
            editor = await _context.ApplicationUsers.FindAsync(new object[] { request.IntroductionEditorId.Value }, cancellationToken);
            Guard.Against.NotFound(request.IntroductionEditorId.Value, editor);
        }
        var entity = new Source
        {
            AuthorId = author.Id,
            CenturyId = century.Id,
            Introduction = request.Introduction,
            SourceTitleInArabic = request.SourceTitleInArabic,
            SourceTitleInSyriac = request.SourceTitleInSyriac,
            SourceTitleInForeignLanguage = request.SourceTitleInForeignLanguage,
            AdditionalInfo = request.AdditionalInfo,
            IntroductionEditorId = editor?.Id,
            Publications = new(),
            OtherAttachments = new()
        };
        _context.Sources.Add(entity);
        await _context.SaveChangesAsync(cancellationToken); 
        AddSourceDates(request.SourceDates, entity.Id);
        AddCoverPhoto(request.CoverPhoto, entity.Id);
        AddAttachments(request.OtherAttachments, entity.Id);
        AddPublications(request.Publications, entity.Id);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
    private void AddSourceDates(IEnumerable<SourceDateDto>? dates, int sourceId)
    {
        if (dates is null || !dates.Any()) return;
        var sourceDates = dates.Select(d => new SourceDate
        {
            SourceId = sourceId,
            DateFormatId = d.DateFormatId,
            FromYear = d.FromYear,
            ToYear = d.ToYear
        });
        _context.SourceDates.AddRange(sourceDates);
    }
    private void AddCoverPhoto(CoverPhotoDto? cover, int sourceId)
    {
        if (cover == null) return;
        _context.CoverPhotos.Add(new CoverPhoto
        {
            FileName = cover.FileName,
            FilePath = cover.FilePath,
            FileExtension = cover.FileExtension,
            SourceId = sourceId
        });
    }
    private void AddAttachments(IEnumerable<AttachmentDto>? attachments, int sourceId)
    {
        if (attachments is null) return;
        var files = attachments.Select(a => new Attachment
        {
            FileName = a.FileName,
            FilePath = a.FilePath,
            FileExtension = a.FileExtension,
            SourceId = sourceId
        });
        _context.Attachments.AddRange(files);
    }
    private void AddPublications(IEnumerable<PublicationDto>? publications, int sourceId)
    {
        if (publications is null) return;
        var pubs = publications.Select(p => new Publication
        {
            Url = p.Url,
            Description = p.Description,
            SourceId = sourceId
        });
        _context.Publications.AddRange(pubs);
    }
}
