using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ardalis.GuardClauses;
namespace SyriacSources.Backend.Application.Centuries.Queries.GetCentury;
public record GetCenturyQuery(int Id) : IRequest<CenturyDto>;
public class GetCenturyQueryHandler : IRequestHandler<GetCenturyQuery, CenturyDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetCenturyQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<CenturyDto> Handle(GetCenturyQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Centuries
            .Where(c => c.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<CenturyDto>(entity);
    }
}
