using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Exceptions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Sources;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.Sources.Queries;
public record GetSource(int Id) : IRequest<SourceDto>;
public class GetSourceHandler : IRequestHandler<GetSource, SourceDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetSourceHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<SourceDto> Handle(GetSource request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sources
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Century)
            .Include(x => x.IntroductionEditor)
            .Include(x => x.Publications)
            .Include(x => x.OtherAttachments)
            .Include(x => x.CoverPhoto)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Source), request.Id.ToString());
        }
        return _mapper.Map<SourceDto>(entity);
    }
}
