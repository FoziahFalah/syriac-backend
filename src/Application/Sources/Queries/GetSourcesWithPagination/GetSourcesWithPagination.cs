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
   
    public int? AuthorId { get; init; }
    public int? CenturyId { get; init; }
}
public class GetSourcesWithPaginationHandler : IRequestHandler<GetSourcesWithPagination, PaginatedList<SourceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSourcesWithPaginationHandler(IApplicationDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
       
        if (request.AuthorId.HasValue)
            query = query.Where(x => x.AuthorId == request.AuthorId.Value);
        if (request.CenturyId.HasValue)
            query = query.Where(x => x.CenturyId == request.CenturyId.Value);
        return await query
            .ProjectTo<SourceDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}











