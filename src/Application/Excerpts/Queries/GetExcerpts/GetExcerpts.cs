using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Excerpts.Dots;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Excerpts.Queries.GetExcerpts;

public class GetExcerpts : IRequest<List<ExcerptDto>>
{
}
public class GetExcerptsHandler : IRequestHandler<GetExcerpts, List<ExcerptDto>>
{
    private readonly IApplicationDbContext _context;
    public GetExcerptsHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<ExcerptDto>> Handle(GetExcerpts request, CancellationToken cancellationToken)
    {
        var excerpts = await _context.Excerpts
            .Include(e => e.ExcerptTexts).ThenInclude(t => t.Language)
            .Include(e => e.ExcerptTexts).ThenInclude(t => t.Editor)
            .Include(e => e.ExcerptTexts).ThenInclude(t => t.Reviewer)
            .Include(e => e.ExcerptTexts).ThenInclude(t => t.Translator)
            .Include(e => e.ExcerptDates).ThenInclude(d => d.DateFormat)
            .Include(e => e.ExcerptComments).ThenInclude(c => c.ApplicationUser)
            .ToListAsync(cancellationToken);
        return excerpts.Select(excerpt => new ExcerptDto
        {
            Id = excerpt.Id,
            AdditionalInfo = excerpt.AdditionalInfo,
            SyriacText = MapText(excerpt.ExcerptTexts.FirstOrDefault(t =>
                t.Language.Name != "Arabic" && t.Language.Name != "English")),
            ArabicTranslation = MapText(excerpt.ExcerptTexts.FirstOrDefault(t =>
                t.Language.Name == "Arabic")),
            ForeignTranslation = MapText(excerpt.ExcerptTexts.FirstOrDefault(t =>
                t.Language.Name == "English")),
            Dates = excerpt.ExcerptDates.Select(d => new ExcerptDateDto
            {
                DateFormatName = d.DateFormat.Period,
                FromYear = d.FromYear,
                ToYear = d.ToYear
            }).ToList(),
            Comments = excerpt.ExcerptComments.Select(c => new ExcerptCommentDto
            {
                Details = c.Details,
                AuthorName = c.ApplicationUser.FullNameAR ?? "—"
            }).ToList()
        }).ToList();
    }
    private static ExcerptTextDto? MapText(ExcerptText? text)
    {
        if (text == null) return null;
        return new ExcerptTextDto
        {
            Text = text.Text,
            LanguageName = text.Language?.Name,
            EditorName = text.Editor?.FullNameAR,
            ReviewerName = text.Reviewer?.FullNameAR,
            TranslatorName = text.Translator?.FullNameAR
        };
    }
}





