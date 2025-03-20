using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace SyriacSources.Backend.Application.Centuries.Queries.GetCenturies;
public record GetCenturiesQuery() : IRequest<List<CenturyDto>>;
public class GetCenturiesQueryHandler : IRequestHandler<GetCenturiesQuery, List<CenturyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetCenturiesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<CenturyDto>> Handle(GetCenturiesQuery request, CancellationToken cancellationToken)
    {
        var centuries = await _context.Centuries.ToListAsync(cancellationToken);
        return _mapper.Map<List<CenturyDto>>(centuries);
    }
}
