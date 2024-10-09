
using Microsoft.AspNetCore.Identity;

namespace SyriacSources.Backend.Infrastructure.Identity;
public class ApplicationRoleClaim : IdentityRoleClaim<string>
{
    public virtual ApplicationRole ApplicationRole => new ApplicationRole();
}
