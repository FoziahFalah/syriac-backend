using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Sources;
namespace SyriacSources.Backend.Application.Sources.Queries;
public record GetSourcesQuery : IRequest<List<SourceDto>>;
public class GetSourcesQueryHandler : IRequestHandler<GetSourcesQuery, List<SourceDto>>
{
    private readonly IApplicationDbContext _context;
    public GetSourcesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<SourceDto>> Handle(GetSourcesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Sources
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Century)
            .Include(x => x.IntroductionEditor)
            .Include(x => x.Publications)
            .Include(x => x.OtherAttachments)
            .Include(x => x.CoverPhoto)
            .Select(x => new SourceDto
            {
                Id = x.Id,
                AuthorName = x.Author.Name,
                CenturyName = x.Century.Name,
                DocumentedOnHijri = x.DocumentedOnHijri,
                DocumentedOnGregorian = x.DocumentedOnGregorian,
                Introduction = x.Introduction,
                SourceTitleInArabic = x.SourceTitleInArabic,
                SourceTitleInSyriac = x.SourceTitleInSyriac,
                SourceTitleInForeignLanguage = x.SourceTitleInForeignLanguage,
                IntroductionEditorName = x.IntroductionEditor.FullNameAR,
                AdditionalInfo = x.AdditionalInfo,
                Created = x.Created,
                CreatedBy = x.CreatedBy,
                LastModified = x.LastModified,
                LastModifiedBy = x.LastModifiedBy,
                Publications = x.Publications.Select(p => new PublicationDto
                {
                    Url = p.Url,
                    Description = p.Description
                }).ToList(),
                OtherAttachments = x.OtherAttachments.Select(a => new AttachmentDto
                {
                    FileName = a.FileName,
                    FilePath = a.FilePath,
                    FileExtension = a.FileExtension
                }).ToList(),
                CoverPhoto = x.CoverPhoto != null
                    ? new CoverPhotoDto
                    {
                        FileName = x.CoverPhoto.FileName,
                        FilePath = x.CoverPhoto.FilePath,
                        FileExtension = x.CoverPhoto.FileExtension
                    }
                    : null
            })
            .ToListAsync(cancellationToken);
    }
}
