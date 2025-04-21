using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.Sources.Commands.CreateSource;
public class CreateSourceCommand : IRequest<int>
{
    public string AuthorName { get; set; } = string.Empty;
    public string CenturyName { get; set; } = string.Empty;
    public DateTime DocumentedOnHijri { get; set; }
    public DateTime DocumentedOnGregorian { get; set; }
    public string? Introduction { get; set; }
    public string? SourceTitleInArabic { get; set; }
    public string? SourceTitleInSyriac { get; set; }
    public string? SourceTitleInForeignLanguage { get; set; }
    public string? IntroductionEditorName { get; set; }
    public string? AdditionalInfo { get; set; }
    public AttachmentDto? CoverPhoto { get; set; } // بدلاً من CoverPhotoId
    public List<AttachmentDto>? OtherAttachments { get; set; } 
    public List<PublicationDto> ? Publications { get; set; } 
}
public class AttachmentDto
{
    public string? FileName { get; set; }
    public string? FilePath { get; set; } 
    public string? FileExtension { get; set; }
}
public class PublicationDto
{
    public string? Description { get; set; }
    public string Url { get; set; } = string.Empty;
}
public class CreateSourceCommandHandler : IRequestHandler<CreateSourceCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreateSourceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateSourceCommand request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.Name == request.AuthorName, cancellationToken);
        var century = await _context.Centuries
            .FirstOrDefaultAsync(c => c.Name == request.CenturyName, cancellationToken);
        if (author == null || century == null)
            throw new Exception("الاسم غير موجود في قاعدة البيانات");
        ApplicationUser? editor = null;
        if (!string.IsNullOrEmpty(request.IntroductionEditorName))
        {
            editor = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u =>
                    u.FullNameAR == request.IntroductionEditorName || u.FullNameEN == request.IntroductionEditorName,
                    cancellationToken);
        }
        // إنشاء المصدر بدون المرفقات
        var entity = new Source
        {
            AuthorId = author.Id,
            CenturyId = century.Id,
            DocumentedOnHijri = request.DocumentedOnHijri,
            DocumentedOnGregorian = request.DocumentedOnGregorian,
            Introduction = request.Introduction,
            SourceTitleInArabic = request.SourceTitleInArabic,
            SourceTitleInSyriac = request.SourceTitleInSyriac,
            SourceTitleInForeignLanguage = request.SourceTitleInForeignLanguage,
            AdditionalInfo = request.AdditionalInfo,
            IntroductionEditor = editor ?? new ApplicationUser(),
            Publications = new List<Publication>(),
            OtherAttachments = new List<Attachment>()
        };
        _context.Sources.Add(entity);
        await _context.SaveChangesAsync(cancellationToken); // يتم الحفظ للحصول على ID
        // إضافة صورة الغلاف بعد الحفظ وربطها بالـ SourceId
        if (request.CoverPhoto is not null)
        {
            var cover = new Attachment
            {
                FileName = request.CoverPhoto.FileName,
                FilePath = request.CoverPhoto.FilePath,
                FileExtension = request.CoverPhoto.FileExtension,
                SourceId = entity.Id
            };
            entity.CoverPhoto = cover;
        }
        // إضافة المرفقات الأخرى بعد الحفظ
        if (request.OtherAttachments is not null)
        {
            foreach (var attach in request.OtherAttachments)
            {
                entity.OtherAttachments.Add(new Attachment
                {
                    FileName = attach.FileName,
                    FilePath = attach.FilePath,
                    FileExtension = attach.FileExtension,
                    SourceId = entity.Id
                });
            }
        }
        // إضافة النشرات
        if (request.Publications is not null)
        {
            foreach (var pub in request.Publications)
            {
                entity.Publications.Add(new Publication
                {
                    Description = pub.Description,
                    Url = pub.Url
                });
            }
        }
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
