using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ardalis.GuardClauses;
namespace SyriacSources.Backend.Application.DateFormats.Queries.GetDateFormat;
public record GetDateFormatQuery(int Id) : IRequest<DateFormatDto>;
public class GetDateFormatQueryHandler : IRequestHandler<GetDateFormatQuery, DateFormatDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetDateFormatQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<DateFormatDto> Handle(GetDateFormatQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.DateFromats
            .Where(d => d.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<DateFormatDto>(entity);
    }
}
