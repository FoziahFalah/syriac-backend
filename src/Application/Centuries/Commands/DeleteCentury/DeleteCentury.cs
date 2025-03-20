using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using Ardalis.GuardClauses;
namespace SyriacSources.Backend.Application.Centuries.Commands.DeleteCentury;
public record DeleteCenturyCommand(int Id) : IRequest<Unit>;
public class DeleteCenturyCommandHandler : IRequestHandler<DeleteCenturyCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public DeleteCenturyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(DeleteCenturyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Centuries.FindAsync(request.Id);
        Guard.Against.NotFound(request.Id, entity);
        _context.Centuries.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
