using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Ganss.Xss;
namespace SyriacSources.Backend.Application.Sources.Commands.UpdateSource;

public record UploadCoverPhotoCommand : IRequest<int>
{
    public int SourceId { get; init; }
    public IFormFile? File { get; init; }
}
public class UploadCoverPhotoHandler : IRequestHandler<UploadCoverPhotoCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IFileServices _fileService;
    private readonly IHtmlSanitizer _htmlSanitizer;

    public UploadCoverPhotoHandler(IApplicationDbContext context, IFileServices fileService, IHtmlSanitizer htmlSanitizer)
    {
        _context = context;
        _fileService = fileService;
        _htmlSanitizer = htmlSanitizer;
    }

    public async Task<int> Handle(UploadCoverPhotoCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null)
            return 0;

        var fileNameSanitized = _htmlSanitizer.Sanitize(request.File.FileName);

        using var fileStream = request.File.OpenReadStream();

        var filePath = await _fileService.SaveFileAsync(fileNameSanitized, fileStream);

        CoverPhoto file = new CoverPhoto
        {
            SourceId = request.SourceId,
            FileName = fileNameSanitized,
            FilePath = filePath,
            FileExtension = Path.GetExtension(fileNameSanitized)
        };

        _context.CoverPhotos.Add(file);

        var result = await _context.SaveChangesAsync(cancellationToken);

        return result;
        
    }
}
