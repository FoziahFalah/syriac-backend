using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Sources.Commands.DeleteFiles
{
    // لحذف مرفق (Attachment)
    public record DeleteAttachmentCommand(int AttachmentId) : IRequest;
    public class DeleteAttachmentHandler : IRequestHandler<DeleteAttachmentCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteAttachmentHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
        {
            var attachment = await _context.Attachments
                .FirstOrDefaultAsync(a => a.Id == request.AttachmentId, cancellationToken);
            if (attachment == null)
                throw new NotFoundException("Attachment", request.AttachmentId.ToString());
            _context.Attachments.Remove(attachment);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        Task IRequestHandler<DeleteAttachmentCommand>.Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
    // لحذف صورة الغلاف (CoverPhoto)
    public record DeleteCoverPhotoCommand(int SourceId) : IRequest;
    public class DeleteCoverPhotoHandler : IRequestHandler<DeleteCoverPhotoCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteCoverPhotoHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteCoverPhotoCommand request, CancellationToken cancellationToken)
        {
            var coverPhoto = await _context.CoverPhotos
                .FirstOrDefaultAsync(c => c.SourceId == request.SourceId, cancellationToken);
            if (coverPhoto == null)
                throw new NotFoundException("CoverPhoto", request.SourceId.ToString());
            _context.CoverPhotos.Remove(coverPhoto);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        Task IRequestHandler<DeleteCoverPhotoCommand>.Handle(DeleteCoverPhotoCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
