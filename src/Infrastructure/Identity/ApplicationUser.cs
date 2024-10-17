using Microsoft.AspNetCore.Identity;
using SyriacSources.Backend.Domain.Common;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public int ContributorId { get; set; }
    public Contributor Contributor { get; set; } = new Contributor();

}
