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
    private readonly IMapper _mapper;
    public GetSourcesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<SourceDto>> Handle(GetSourcesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Sources
            .AsNoTracking()
            .ProjectTo<SourceDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
