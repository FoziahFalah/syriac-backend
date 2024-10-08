using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Domain.Entities;
public class ExcerptText : BaseAuditableEntity
{
    public int ExcerptId { get; set; }
    public string? Text { get; set; }
    public int LanguageId{ get; set; }
    public Language Language { get; set; } = new Language();
    public int EditorId{ get; set; }
    public Contributor Editor { get; set; } = new Contributor();
    public int ReviewerId{ get; set; }
    public Contributor Reviewer { get; set; } = new Contributor();
    public int TranslatorId { get; set; }
    public Contributor Translator { get; set; } = new Contributor();

}
