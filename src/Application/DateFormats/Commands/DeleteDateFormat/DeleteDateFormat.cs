using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using Ardalis.GuardClauses;
namespace SyriacSources.Backend.Application.DateFormats.Commands.DeleteDateFormat;
public record DeleteDateFormatCommand(int Id) : IRequest<Unit>;
public class DeleteDateFormatCommandHandler : IRequestHandler<DeleteDateFormatCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public DeleteDateFormatCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(DeleteDateFormatCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.DateFromats.FindAsync(request.Id);
        Guard.Against.NotFound(request.Id, entity);
        _context.DateFromats.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
