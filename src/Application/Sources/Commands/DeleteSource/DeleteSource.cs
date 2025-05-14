using MediatR;
using SyriacSources.Backend.Application.Common.Exceptions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.Sources.Commands;
public class DeleteSource : IRequest
{
    public DeleteSource(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
public class DeleteSourceHandler : IRequestHandler<DeleteSource>
{
    private readonly IApplicationDbContext _context;
    public DeleteSourceHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(DeleteSource request, CancellationToken cancellationToken)
    {
        var source = await _context.Sources
            .Include(s => s.SourceDates)
            .Include(s => s.Publications)
            .Include(s => s.OtherAttachments)
            .Include(s => s.CoverPhoto)
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (source == null)
            throw new NotFoundException(nameof(Source), request.Id.ToString());
       
        _context.SourceDates.RemoveRange(source.SourceDates);
        
        _context.Attachments.RemoveRange(source.OtherAttachments);
      
        _context.Publications.RemoveRange(source.Publications);
       
        if (source.CoverPhoto != null)
            _context.CoverPhotos.Remove(source.CoverPhoto);
       
        _context.Sources.Remove(source);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    Task IRequestHandler<DeleteSource>.Handle(DeleteSource request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}
