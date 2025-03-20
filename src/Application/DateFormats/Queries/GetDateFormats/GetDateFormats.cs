using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace SyriacSources.Backend.Application.DateFormats.Queries.GetDateFormats;
public record GetDateFormatsQuery() : IRequest<List<DateFormatDto>>;
public class GetDateFormatsQueryHandler : IRequestHandler<GetDateFormatsQuery, List<DateFormatDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetDateFormatsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<DateFormatDto>> Handle(GetDateFormatsQuery request, CancellationToken cancellationToken)
    {
        var dateFormats = await _context.DateFromats.ToListAsync(cancellationToken);
        return _mapper.Map<List<DateFormatDto>>(dateFormats);
    }
}








