using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Domain.Entities;
public class ApplicationUser : BaseAuditableEntity
{
    public int IdentityApplicationUserId { get; set; }
    public string? FullNameEN { get; set; }
    public string? FullNameAR { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public UserType UserType { get; set; }
    public List<ExcerptText> EditedExcerpts { get; set; } = null!;
    public List<ExcerptText> ReviewedExcerpts { get; set; } = null!;
    public List<ExcerptText> TranslatedExcerpts { get; set; } = null!;
}






