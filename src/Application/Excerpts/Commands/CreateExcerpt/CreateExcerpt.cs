using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Excerpts.Commands.CreateExcerpt;
public class CreateExcerpt : IRequest<int>
{
    public int SourceId { get; set; }
    public string? AdditionalInfo { get; set; }
    public ExcerptTextInput SyriacText { get; set; } = new();
    public ExcerptTextInput ArabicTranslation { get; set; } = new();
    public ExcerptTextInput ForeignTranslation { get; set; } = new();
    public List<ExcerptDateInput> Dates { get; set; } = new();
    public List<ExcerptCommentInput> Comments { get; set; } = new();
}
public class ExcerptTextInput
{
    public string? Text { get; set; }
    public int LanguageId { get; set; }
    public int EditorId { get; set; }
    public int ReviewerId { get; set; }
    public int TranslatorId { get; set; }
}
public class ExcerptDateInput
{
    public int DateFormatId { get; set; }
    public int FromYear { get; set; }
    public int ToYear { get; set; }
}
public class ExcerptCommentInput
{
    public int ApplicationUserId { get; set; }
    public string Details { get; set; } = string.Empty;
}
public class CreateExcerptHandler : IRequestHandler<CreateExcerpt, int>
{
    private readonly IApplicationDbContext _context;
    public CreateExcerptHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateExcerpt request, CancellationToken cancellationToken)
    {
        var excerpt = new Excerpt
        {
            SourceId = request.SourceId,
            AdditionalInfo = request.AdditionalInfo,
            ExcerptTexts = new List<ExcerptText>
            {
                new()
                {
                    Text = request.SyriacText.Text,
                    LanguageId = request.SyriacText.LanguageId,
                    EditorId = request.SyriacText.EditorId,
                    ReviewerId = request.SyriacText.ReviewerId,
                    TranslatorId = request.SyriacText.TranslatorId
                },
                new()
                {
                    Text = request.ArabicTranslation.Text,
                    LanguageId = request.ArabicTranslation.LanguageId,
                    EditorId = request.ArabicTranslation.EditorId,
                    ReviewerId = request.ArabicTranslation.ReviewerId,
                    TranslatorId = request.ArabicTranslation.TranslatorId
                },
                new()
                {
                    Text = request.ForeignTranslation.Text,
                    LanguageId = request.ForeignTranslation.LanguageId,
                    EditorId = request.ForeignTranslation.EditorId,
                    ReviewerId = request.ForeignTranslation.ReviewerId,
                    TranslatorId = request.ForeignTranslation.TranslatorId
                }
            },
            ExcerptDates = request.Dates.Select(d => new ExcerptDate
            {
                DateFormatId = d.DateFormatId,
                FromYear = d.FromYear,
                ToYear = d.ToYear
            }).ToList(),
            ExcerptComments = request.Comments.Select(c => new ExcerptComment
            {
                ApplicationUserId = c.ApplicationUserId,
                Details = c.Details
            }).ToList()
        };
        _context.Excerpts.Add(excerpt);
        await _context.SaveChangesAsync(cancellationToken);
        return excerpt.Id;
    }
}








