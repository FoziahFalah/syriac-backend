using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Exceptions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Sources;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.Sources.Queries;
public record GetSource(int Id) : IRequest<SourceDto>;
public class GetSourceHandler : IRequestHandler<GetSource, SourceDto>
{
    private readonly IApplicationDbContext _context;
    public GetSourceHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SourceDto> Handle(GetSource request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sources
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Century)
            .Include(x => x.IntroductionEditor)
            .Include(x => x.Publications)
            .Include(x => x.OtherAttachments)
            .Include(x => x.CoverPhoto)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(Source), request.Id.ToString());
        return new SourceDto
        {
            Id = entity.Id,
            AuthorName = entity.Author.Name,
            CenturyName = entity.Century.Name,
            DocumentedOnHijri = entity.DocumentedOnHijri,
            DocumentedOnGregorian = entity.DocumentedOnGregorian,
            Introduction = entity.Introduction,
            SourceTitleInArabic = entity.SourceTitleInArabic,
            SourceTitleInSyriac = entity.SourceTitleInSyriac,
            SourceTitleInForeignLanguage = entity.SourceTitleInForeignLanguage,
            IntroductionEditorName = entity.IntroductionEditor.FullNameAR,
            AdditionalInfo = entity.AdditionalInfo,
            Created = entity.Created,
            CreatedBy = entity.CreatedBy,
            LastModified = entity.LastModified,
            LastModifiedBy = entity.LastModifiedBy,
            Publications = entity.Publications.Select(p => new PublicationDto
            {
                Url = p.Url,
                Description = p.Description
            }).ToList(),
            OtherAttachments = entity.OtherAttachments.Select(a => new AttachmentDto
            {
                FileName = a.FileName,
                FilePath = a.FilePath,
                FileExtension = a.FileExtension
            }).ToList(),
            CoverPhoto = entity.CoverPhoto != null
                ? new CoverPhotoDto
                {
                    FileName = entity.CoverPhoto.FileName,
                    FilePath = entity.CoverPhoto.FilePath,
                    FileExtension = entity.CoverPhoto.FileExtension
                }
                : null
        };
    }
}
