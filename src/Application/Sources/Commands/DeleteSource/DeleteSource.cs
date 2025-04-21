using MediatR;
using SyriacSources.Backend.Application.Common.Exceptions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.Sources.Commands;
public record DeleteSource(int Id) : IRequest;
public class DeleteSourceHandler : IRequestHandler<DeleteSource>
{
    private readonly IApplicationDbContext _context;
    public DeleteSourceHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(DeleteSource request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sources.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(Source), request.Id.ToString());
        _context.Sources.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    Task IRequestHandler<DeleteSource>.Handle(DeleteSource request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}
