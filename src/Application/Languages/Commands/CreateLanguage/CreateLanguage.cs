using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.Languages.Commands.CreateLanguage;
public record CreateLanguageCommand(string Name, string Code) : IRequest<int>;
public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreateLanguageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
    {
        var language = new Language { Name = request.Name, Code = request.Code };
        _context.Languages.Add(language);
        await _context.SaveChangesAsync(cancellationToken);
        return language.Id;
    }
}
