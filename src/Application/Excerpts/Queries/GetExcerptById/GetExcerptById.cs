using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Excerpts.Dots;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Excerpts.Queries.GetExcerptById;

public class GetExcerptById : IRequest<ExcerptDto>
{
    public int Id { get; set; }
}
public class GetExcerptByIdHandler : IRequestHandler<GetExcerptById, ExcerptDto?>
{
    private readonly IApplicationDbContext _context;
    public GetExcerptByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ExcerptDto?> Handle(GetExcerptById request, CancellationToken cancellationToken)
    {
        var excerpt = await _context.Excerpts
            .Include(e => e.ExcerptTexts).ThenInclude(t => t.Language)
            .Include(e => e.ExcerptTexts).ThenInclude(t => t.Editor)
            .Include(e => e.ExcerptTexts).ThenInclude(t => t.Reviewer)
            .Include(e => e.ExcerptTexts).ThenInclude(t => t.Translator)
            .Include(e => e.ExcerptDates).ThenInclude(d => d.DateFormat)
            .Include(e => e.ExcerptComments).ThenInclude(c => c.ApplicationUser)
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (excerpt == null)
            return null;
        // استخراج النصوص حسب اللغة
        var syriac = excerpt.ExcerptTexts.FirstOrDefault(t =>
            t.Language.Name != "Arabic" && t.Language.Name != "English");
        var arabic = excerpt.ExcerptTexts.FirstOrDefault(t =>
            t.Language.Name == "Arabic");
        var english = excerpt.ExcerptTexts.FirstOrDefault(t =>
            t.Language.Name == "English");
        return new ExcerptDto
        {
            Id = excerpt.Id,
            SourceId = excerpt.SourceId,
            AdditionalInfo = excerpt.AdditionalInfo,
            SyriacText = MapText(syriac),
            ArabicTranslation = MapText(arabic),
            ForeignTranslation = MapText(english),
            Dates = excerpt.ExcerptDates.Select(d => new ExcerptDateDto
            {
                DateFormatId = d.DateFormatId,
                DateFormatName = $"{d.DateFormat.Format} - {d.DateFormat.Period}",
                FromYear = d.FromYear,
                ToYear = d.ToYear
            }).ToList(),
            Comments = excerpt.ExcerptComments.Select(c => new ExcerptCommentDto
            {
                Details = c.Details,
                AuthorName = c.ApplicationUser.FullNameAR ?? "—"
            }).ToList()
        };
    }
    private static ExcerptTextDto? MapText(ExcerptText? t)
    {
        if (t == null) return null;
        return new ExcerptTextDto
        {
            Text = t.Text,
            LanguageName = t.Language?.Name,
            EditorName = t.Editor?.FullNameAR,
            ReviewerName = t.Reviewer?.FullNameAR,
            TranslatorName = t.Translator?.FullNameAR
        };
    }
}


