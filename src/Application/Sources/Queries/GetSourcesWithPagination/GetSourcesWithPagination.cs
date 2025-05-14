using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Mappings;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.Sources;
namespace SyriacSources.Backend.Application.Sources.Queries;
public class GetSourcesWithPagination : IRequest<PaginatedList<SourceDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    // فلترة
    public int? AuthorId { get; init; }
    public int? CenturyId { get; init; }
}
public class GetSourcesWithPaginationHandler : IRequestHandler<GetSourcesWithPagination, PaginatedList<SourceDto>>
{
    private readonly IApplicationDbContext _context;
    public GetSourcesWithPaginationHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<SourceDto>> Handle(GetSourcesWithPagination request, CancellationToken cancellationToken)
    {
        var query = _context.Sources
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Century)
            .Include(x => x.IntroductionEditor)
            .Include(x => x.Publications)
            .Include(x => x.OtherAttachments)
            .Include(x => x.CoverPhoto)
            .Include(x => x.SourceDates)
                .ThenInclude(d => d.DateFormat)
            .AsQueryable();
        // الفلاتر
        if (request.AuthorId.HasValue)
            query = query.Where(x => x.AuthorId == request.AuthorId.Value);
        if (request.CenturyId.HasValue)
            query = query.Where(x => x.CenturyId == request.CenturyId.Value);
        return await query
            .OrderByDescending(x => x.Created)
            .Select(x => new SourceDto
            {
                Id = x.Id,
                AuthorId = x.AuthorId,
                AuthorName = x.Author.Name,
                CenturyId = x.CenturyId,
                CenturyName = x.Century.Name,
                IntroductionEditorId = x.IntroductionEditorId,
                IntroductionEditorName = x.IntroductionEditor != null ? x.IntroductionEditor.FullNameAR : null,
                Introduction = x.Introduction,
                SourceTitleInArabic = x.SourceTitleInArabic,
                SourceTitleInSyriac = x.SourceTitleInSyriac,
                SourceTitleInForeignLanguage = x.SourceTitleInForeignLanguage,
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
                    : null,
                SourceDates = x.SourceDates.Select(d => new SourceDateDto
                {
                    DateFormatId = d.DateFormatId,
                    FromYear = d.FromYear,
                    ToYear = d.ToYear,
                    Format = d.DateFormat != null ? d.DateFormat.Format : null,
                    Period = d.DateFormat != null ? d.DateFormat.Period : null
                }).ToList()
            })
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}











