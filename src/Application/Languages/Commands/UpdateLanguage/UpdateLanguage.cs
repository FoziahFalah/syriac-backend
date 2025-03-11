using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ardalis.GuardClauses;
namespace SyriacSources.Backend.Application.Languages.Commands.UpdateLanguage;
public record UpdateLanguageCommand(int Id, string Name, string Code) : IRequest<Unit>;
public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public UpdateLanguageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Languages.FindAsync(request.Id);
        Guard.Against.NotFound(request.Id, entity);
        entity.Name = request.Name;
        entity.Code = request.Code;
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}



