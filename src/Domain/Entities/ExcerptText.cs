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
    public ApplicationUser Editor { get; set; } = new ApplicationUser();
    public int ReviewerId{ get; set; }
    public ApplicationUser Reviewer { get; set; } = new ApplicationUser();
    public int TranslatorId { get; set; }
    public ApplicationUser Translator { get; set; } = new ApplicationUser();

}
