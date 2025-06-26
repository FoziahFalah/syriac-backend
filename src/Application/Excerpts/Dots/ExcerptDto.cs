using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Application.Excerpts.Dots;

public class ExcerptDto
{
    public int Id { get; set; }
    public int SourceId { get; set; }
    public string? AdditionalInfo { get; set; }
    public ExcerptTextDto? SyriacText { get; set; }
    public ExcerptTextDto? ArabicTranslation { get; set; }
    public ExcerptTextDto? ForeignTranslation { get; set; }
    public List<ExcerptDateDto> Dates { get; set; } = new();
    public List<ExcerptCommentDto> Comments { get; set; } = new();
}
public class ExcerptTextDto
{
    public string? Text { get; set; }
    public string? LanguageName { get; set; }
    public string? EditorName { get; set; }
    public string? ReviewerName { get; set; }
    public string? TranslatorName { get; set; }
}
public class ExcerptDateDto
{
    public int DateFormatId { get; set; }
    public string DateFormatName { get; set; } = string.Empty;
    public int FromYear { get; set; }
    public int ToYear { get; set; }
}
public class ExcerptCommentDto
{
    public string Details { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
}
