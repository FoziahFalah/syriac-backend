using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using Ardalis.GuardClauses;
namespace SyriacSources.Backend.Application.Languages.Commands.DeleteLanguage;
public record DeleteLanguageCommand(int Id) : IRequest<Unit>;
public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public DeleteLanguageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Languages.FindAsync(request.Id);
        Guard.Against.NotFound(request.Id, entity);
        _context.Languages.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
