using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.Centuries.Commands.CreateCentury;
public record CreateCenturyCommand(string Name) : IRequest<int>;
public class CreateCenturyCommandHandler : IRequestHandler<CreateCenturyCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreateCenturyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateCenturyCommand request, CancellationToken cancellationToken)
    {
        var century = new Century { Name = request.Name };
        _context.Centuries.Add(century);
        await _context.SaveChangesAsync(cancellationToken);
        return century.Id;
    }
}
