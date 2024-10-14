using Microsoft.AspNetCore.Identity;
using SyriacSources.Backend.Domain.Common;

namespace SyriacSources.Backend.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public string? NameAR { get; set; }
    public string? NameEn { get; set; }
    public bool IsActive { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? DeactivatedBy { get; set; }
    public DateTime Deactivatedon { get; set; }

}
