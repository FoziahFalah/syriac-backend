using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.DateFormats.Commands.CreateDateFormat;
public record CreateDateFormatCommand(string Format, string Period) : IRequest<int>;
public class CreateDateFormatCommandHandler : IRequestHandler<CreateDateFormatCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreateDateFormatCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateDateFormatCommand request, CancellationToken cancellationToken)
    {
        var dateFormat = new DateFormat { Format = request.Format, Period = request.Period };
        _context.DateFromats.Add(dateFormat);
        await _context.SaveChangesAsync(cancellationToken);
        return dateFormat.Id;
    }
}
