using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Excerpts.Commands.DeleteExcerpt;

public class DeleteExcerpt : IRequest<int>
{
    public int Id { get; set; }
}
public class DeleteExcerptHandler : IRequestHandler<DeleteExcerpt, int>
{
    private readonly IApplicationDbContext _context;
    public DeleteExcerptHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(DeleteExcerpt request, CancellationToken cancellationToken)
    {
        var excerpt = await _context.Excerpts
            .Include(e => e.ExcerptTexts)
            .Include(e => e.ExcerptDates)
            .Include(e => e.ExcerptComments)
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (excerpt is null)
            throw new Exception("Excerpt not found");
        _context.Excerpts.Remove(excerpt);
        await _context.SaveChangesAsync(cancellationToken);
        return excerpt.Id;
    }
}
