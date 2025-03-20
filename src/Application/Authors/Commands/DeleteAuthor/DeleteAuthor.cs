using MediatR;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
namespace SyriacSources.Backend.Application.Authors.Commands.DeleteAuthor;
public record DeleteAuthorCommand(int Id) : IRequest;
public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteAuthorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (author == null)
            throw new KeyNotFoundException($"المؤلف بالمعرف {request.Id} غير موجود.");
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
