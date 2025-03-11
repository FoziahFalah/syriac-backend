using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ardalis.GuardClauses;
namespace SyriacSources.Backend.Application.Centuries.Commands.UpdateCentury;
public record UpdateCenturyCommand(int Id, string Name) : IRequest<Unit>;
public class UpdateCenturyCommandHandler : IRequestHandler<UpdateCenturyCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public UpdateCenturyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(UpdateCenturyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Centuries.FindAsync(request.Id);
        Guard.Against.NotFound(request.Id, entity);
        entity.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
