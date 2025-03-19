using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
namespace SyriacSources.Backend.Application.Authors.Queries.GetAuthor;
public record GetAuthorQuery(int Id) : IRequest<AuthorDto>;
public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, AuthorDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAuthorQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<AuthorDto> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (author == null)
            throw new KeyNotFoundException($"المؤلف بالمعرف {request.Id} غير موجود.");
        return _mapper.Map<AuthorDto>(author);
    }
}
