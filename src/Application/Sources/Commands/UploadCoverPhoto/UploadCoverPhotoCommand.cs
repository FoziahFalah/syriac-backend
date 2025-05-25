using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Ganss.Xss;
namespace SyriacSources.Backend.Application.Sources.Commands.UpdateSource;

public record UploadCoverPhotoCommand : IRequest<UploadedFileDto>
{
    public int SourceId { get; init; }
    public IFormFile? File { get; init; }
    public string? FileType { get; init; } // "cover" أو "attachment"
}

public class UploadCoverPhotoHandler : IRequestHandler<UploadCoverPhotoCommand, UploadedFileDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IFileServices _fileService;
    private readonly IHtmlSanitizer _htmlSanitizer;
    public UploadCoverPhotoHandler(
        IApplicationDbContext context,
        IFileServices fileService,
        IHtmlSanitizer htmlSanitizer)
    {
        _context = context;
        _fileService = fileService;
        _htmlSanitizer = htmlSanitizer;
    }
    public async Task<UploadedFileDto> Handle(UploadCoverPhotoCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null || string.IsNullOrEmpty(request.FileType))
            throw new ArgumentException("الملف أو نوعه غير صالح");
        var fileNameSanitized = _htmlSanitizer.Sanitize(request.File.FileName);
        using var fileStream = request.File.OpenReadStream();
        var filePath = await _fileService.SaveFileAsync(fileNameSanitized, fileStream);
        var fileExtension = Path.GetExtension(fileNameSanitized);
        if (request.FileType.ToLower() == "attachment")
        {
            var attachment = new Attachment
            {
                SourceId = request.SourceId,
                FileName = fileNameSanitized,
                FilePath = filePath,
                FileExtension = fileExtension
            };
            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync(cancellationToken);
            return new UploadedFileDto
            {
                Id = attachment.Id,
                FileName = fileNameSanitized,
                FilePath = filePath,
                FileExtension = fileExtension
            };
        }
        else if (request.FileType.ToLower() == "cover")
        {
            var existing = await _context.CoverPhotos
                .FirstOrDefaultAsync(c => c.SourceId == request.SourceId, cancellationToken);
            if (existing != null)
                _context.CoverPhotos.Remove(existing);
            var newCover = new CoverPhoto
            {
                SourceId = request.SourceId,
                FileName = fileNameSanitized,
                FilePath = filePath,
                FileExtension = fileExtension
            };
            _context.CoverPhotos.Add(newCover);
            await _context.SaveChangesAsync(cancellationToken);
            return new UploadedFileDto
            {
                Id = newCover.Id,
                FileName = fileNameSanitized,
                FilePath = filePath,
                FileExtension = fileExtension
            };
        }
        throw new ArgumentException("نوع الملف غير مدعوم");
    }
}


