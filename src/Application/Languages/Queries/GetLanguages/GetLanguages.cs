using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace SyriacSources.Backend.Application.Languages.Queries.GetLanguages;
public record GetLanguagesQuery() : IRequest<List<LanguageDto>>;
public class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, List<LanguageDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetLanguagesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<LanguageDto>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
    {
        var languages = await _context.Languages.ToListAsync(cancellationToken);
        return _mapper.Map<List<LanguageDto>>(languages);
    }
}
