using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace SyriacSources.Backend.Application.Authors.Queries.GetAuthors;
public record GetAuthorsQuery() : IRequest<List<AuthorDto>>;
public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, List<AuthorDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAuthorsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _context.Authors.ToListAsync(cancellationToken);
        return _mapper.Map<List<AuthorDto>>(authors);
    }
}
