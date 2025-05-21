
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Exceptions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
namespace SyriacSources.Backend.Application.Sources.Commands.UpdateSource;

public class UpdateSourceCommand : IRequest
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public int CenturyId { get; set; }
    public int? IntroductionEditorId { get; set; }
    public List<SourceDateDto>? SourceDates { get; set; }
    public string? Introduction { get; set; }
    public string? SourceTitleInArabic { get; set; }
    public string? SourceTitleInSyriac { get; set; }
    public string? SourceTitleInForeignLanguage { get; set; }
    public string? AdditionalInfo { get; set; }
    public CoverPhotoDto? CoverPhoto { get; set; }
    public List<AttachmentDto>? OtherAttachments { get; set; }
    public List<PublicationDto>? Publications { get; set; }
}
public class UpdateSourceHandler : IRequestHandler<UpdateSourceCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateSourceHandler(IApplicationDbContext context)
    {
        _context = context;
    }
        public async Task<Unit> Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Sources
                .Include(s => s.SourceDates)
                .Include(s => s.CoverPhoto)
                .Include(s => s.OtherAttachments)
                .Include(s => s.Publications)
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
            Guard.Against.NotFound(request.Id, entity);
            var author = await _context.Authors.FindAsync(new object[] { request.AuthorId }, cancellationToken);
            var century = await _context.Centuries.FindAsync(new object[] { request.CenturyId }, cancellationToken);
            Guard.Against.NotFound(request.AuthorId, author);
            Guard.Against.NotFound(request.CenturyId, century);
            ApplicationUser? editor = null;
            if (request.IntroductionEditorId.HasValue)
            {
                editor = await _context.ApplicationUsers.FindAsync(new object[] { request.IntroductionEditorId.Value }, cancellationToken);
                Guard.Against.NotFound(request.IntroductionEditorId.Value, editor);
            }
            entity.AuthorId = author.Id;
            entity.CenturyId = century.Id;
            entity.Introduction = request.Introduction;
            entity.SourceTitleInArabic = request.SourceTitleInArabic;
            entity.SourceTitleInSyriac = request.SourceTitleInSyriac;
            entity.SourceTitleInForeignLanguage = request.SourceTitleInForeignLanguage;
            entity.AdditionalInfo = request.AdditionalInfo;
            entity.IntroductionEditorId = editor?.Id;
            
            _context.SourceDates.RemoveRange(entity.SourceDates);
            if (request.SourceDates?.Any() == true)
            {
                var newDates = request.SourceDates.Select(d => new SourceDate
                {
                    SourceId = entity.Id,
                    DateFormatId = d.DateFormatId,
                    FromYear = d.FromYear,
                    ToYear = d.ToYear
                });
                _context.SourceDates.AddRange(newDates);
            }
           
            if (entity.CoverPhoto != null)
                _context.CoverPhotos.Remove(entity.CoverPhoto);
            if (request.CoverPhoto != null)
            {
                _context.CoverPhotos.Add(new CoverPhoto
                {
                    FileName = request.CoverPhoto.FileName,
                    FilePath = request.CoverPhoto.FilePath,
                    FileExtension = request.CoverPhoto.FileExtension,
                    SourceId = entity.Id
                });
            }
            
            _context.Attachments.RemoveRange(entity.OtherAttachments);
            if (request.OtherAttachments?.Any() == true)
            {
                var files = request.OtherAttachments.Select(a => new Attachment
                {
                    FileName = a.FileName,
                    FilePath = a.FilePath,
                    FileExtension = a.FileExtension,
                    SourceId = entity.Id
                });
                _context.Attachments.AddRange(files);
            }
           
            _context.Publications.RemoveRange(entity.Publications);
            if (request.Publications?.Any() == true)
            {
                var pubs = request.Publications.Select(p => new Publication
                {
                    Url = p.Url,
                    Description = p.Description,
                    SourceId = entity.Id
                });
                _context.Publications.AddRange(pubs);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    Task IRequestHandler<UpdateSourceCommand>.Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}
