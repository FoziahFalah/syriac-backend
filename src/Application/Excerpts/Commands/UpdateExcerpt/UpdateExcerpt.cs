using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Excerpts.Commands.CreateExcerpt;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Excerpts.Commands.UpdateExcerpt;

public class UpdateExcerpt : IRequest<int>
{
    public int Id { get; set; }
    public int SourceId { get; set; }
    public string? AdditionalInfo { get; set; }
    public ExcerptTextInput SyriacText { get; set; } = new();
    public ExcerptTextInput ArabicTranslation { get; set; } = new();
    public ExcerptTextInput ForeignTranslation { get; set; } = new();
    public List<ExcerptDateInput> Dates { get; set; } = new();
    public List<ExcerptCommentInput> Comments { get; set; } = new();
}
// :white_check_mark: هنا يبدأ كلاس الهاندلر، لا تخلطين بينهم
public class UpdateExcerptHandler : IRequestHandler<UpdateExcerpt, int>
{
    private readonly IApplicationDbContext _context;
    public UpdateExcerptHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(UpdateExcerpt request, CancellationToken cancellationToken)
    {
        var excerpt = await _context.Excerpts
            .Include(e => e.ExcerptTexts)
            .Include(e => e.ExcerptDates)
            .Include(e => e.ExcerptComments)
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (excerpt is null)
            throw new Exception("Excerpt not found");
        excerpt.SourceId = request.SourceId;
        excerpt.AdditionalInfo = request.AdditionalInfo;
        // :white_check_mark: التحديث الذكي للنصوص
        UpdateOrReplaceText(excerpt, request.SyriacText);
        UpdateOrReplaceText(excerpt, request.ArabicTranslation);
        UpdateOrReplaceText(excerpt, request.ForeignTranslation);
        // :white_check_mark: استبدال التواريخ
        excerpt.ExcerptDates.Clear();
        excerpt.ExcerptDates.AddRange(request.Dates.Select(d => new ExcerptDate
        {
            DateFormatId = d.DateFormatId,
            FromYear = d.FromYear,
            ToYear = d.ToYear
        }));
        // :white_check_mark: استبدال التعليقات
        excerpt.ExcerptComments.Clear();
        excerpt.ExcerptComments.AddRange(request.Comments.Select(c => new ExcerptComment
        {
            ApplicationUserId = c.ApplicationUserId,
            Details = c.Details
        }));
        await _context.SaveChangesAsync(cancellationToken);
        return excerpt.Id;
    }
    // :wrench: دالة خاصة داخل الهاندلر
    private void UpdateOrReplaceText(Excerpt excerpt, ExcerptTextInput input)
    {
        var existing = excerpt.ExcerptTexts
            .FirstOrDefault(t => t.LanguageId == input.LanguageId);
        if (existing != null)
        {
            existing.Text = input.Text ?? existing.Text;
            existing.EditorId = input.EditorId;
            existing.ReviewerId = input.ReviewerId;
            existing.TranslatorId = input.TranslatorId;
        }
        else
        {
            excerpt.ExcerptTexts.Add(new ExcerptText
            {
                LanguageId = input.LanguageId,
                Text = input.Text,
                EditorId = input.EditorId,
                ReviewerId = input.ReviewerId,
                TranslatorId = input.TranslatorId
            });
        }
    }
}




















