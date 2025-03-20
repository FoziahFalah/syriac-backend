using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ardalis.GuardClauses;
namespace SyriacSources.Backend.Application.Languages.Queries.GetLanguage;
public record GetLanguageQuery(int Id) : IRequest<LanguageDto>;
public class GetLanguageQueryHandler : IRequestHandler<GetLanguageQuery, LanguageDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetLanguageQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<LanguageDto> Handle(GetLanguageQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Languages
            .Where(l => l.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        Guard.Against.NotFound(request.Id, entity); // يمنع إرجاع null
        return _mapper.Map<LanguageDto>(entity);
    }
}
