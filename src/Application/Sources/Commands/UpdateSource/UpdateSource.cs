
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Exceptions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
namespace SyriacSources.Backend.Application.Sources.Commands.UpdateSource;

public class UpdateSource : IRequest
{
    public int Id { get; set; }
    public string? AuthorName { get; set; }
    public string? CenturyName { get; set; }
    public DateTime DocumentedOnHijri { get; set; }
    public DateTime DocumentedOnGregorian { get; set; }
    public string? Introduction { get; set; }
    public string? SourceTitleInArabic { get; set; }
    public string? SourceTitleInSyriac { get; set; }
    public string? SourceTitleInForeignLanguage { get; set; }
    public string? IntroductionEditorName { get; set; }
    public string? AdditionalInfo { get; set; }
    public AttachmentDto? CoverPhoto { get; set; }
    public List<AttachmentDto>? OtherAttachments { get; set; }
    public List<PublicationDto>? Publications { get; set; }
}
public class UpdateSourceHandler : IRequestHandler<UpdateSource>
{
    private readonly IApplicationDbContext _context;
    public UpdateSourceHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(UpdateSource request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sources
            .Include(s => s.Publications)
            .Include(s => s.OtherAttachments)
            .Include(s => s.CoverPhoto)
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(Source), request.Id.ToString());
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.Name == request.AuthorName, cancellationToken);
        var century = await _context.Centuries
            .FirstOrDefaultAsync(c => c.Name == request.CenturyName, cancellationToken);
        if (author == null || century == null)
            throw new Exception("اسم المؤلف أو القرن غير موجود.");
        ApplicationUser? editor = null;
        if (!string.IsNullOrEmpty(request.IntroductionEditorName))
        {
            editor = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u =>
                    u.FullNameAR == request.IntroductionEditorName || u.FullNameEN == request.IntroductionEditorName,
                    cancellationToken);
        }
        entity.AuthorId = author.Id;
        entity.CenturyId = century.Id;
        entity.DocumentedOnHijri = request.DocumentedOnHijri;
        entity.DocumentedOnGregorian = request.DocumentedOnGregorian;
        entity.Introduction = request.Introduction;
        entity.SourceTitleInArabic = request.SourceTitleInArabic;
        entity.SourceTitleInSyriac = request.SourceTitleInSyriac;
        entity.SourceTitleInForeignLanguage = request.SourceTitleInForeignLanguage;
        entity.AdditionalInfo = request.AdditionalInfo;
        entity.IntroductionEditor = editor ?? new ApplicationUser();
        // تحديث CoverPhoto فقط إذا كانت البيانات موجودة
        entity.CoverPhoto = request.CoverPhoto != null &&
                            !string.IsNullOrWhiteSpace(request.CoverPhoto.FileName) &&
                            !string.IsNullOrWhiteSpace(request.CoverPhoto.FilePath) &&
                            !string.IsNullOrWhiteSpace(request.CoverPhoto.FileExtension)
            ? new Attachment
            {
                FileName = request.CoverPhoto.FileName,
                FilePath = request.CoverPhoto.FilePath,
                FileExtension = request.CoverPhoto.FileExtension
            }
            : null;
        // تحديث Publications
        entity.Publications.Clear();
        if (request.Publications != null)
        {
            foreach (var pub in request.Publications)
            {
                entity.Publications.Add(new Publication
                {
                    Description = pub.Description,
                    Url = pub.Url
                });
            }
        }
        // تحديث OtherAttachments
        entity.OtherAttachments.Clear();
        if (request.OtherAttachments != null)
        {
            foreach (var att in request.OtherAttachments)
            {
                if (!string.IsNullOrWhiteSpace(att.FileName) &&
                    !string.IsNullOrWhiteSpace(att.FilePath) &&
                    !string.IsNullOrWhiteSpace(att.FileExtension))
                {
                    entity.OtherAttachments.Add(new Attachment
                    {
                        FileName = att.FileName,
                        FilePath = att.FilePath,
                        FileExtension = att.FileExtension
                    });
                }
            }
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
    Task IRequestHandler<UpdateSource>.Handle(UpdateSource request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}
