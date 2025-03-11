using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ardalis.GuardClauses;
namespace SyriacSources.Backend.Application.DateFormats.Commands.UpdateDateFormat;
public record UpdateDateFormatCommand(int Id, string Format, string Period) : IRequest<Unit>;
public class UpdateDateFormatCommandHandler : IRequestHandler<UpdateDateFormatCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public UpdateDateFormatCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(UpdateDateFormatCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.DateFromats.FindAsync(request.Id);
        Guard.Against.NotFound(request.Id, entity);
        entity.Format = request.Format;
        entity.Period = request.Period;
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
