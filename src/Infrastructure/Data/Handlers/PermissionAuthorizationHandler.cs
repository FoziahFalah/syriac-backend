using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Infrastructure.Data.Handlers;
public class PermissionAuthorizationHandler : AuthorizationHandler<OfficeEntryRequirement>
{
    protected override Task HandleRequirementAsync(
      AuthorizationHandlerContext context,
      OfficeEntryRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == "TemporaryBadgeExpiry" &&
                                        c.Issuer == "https://contoso.com"))
        {
            return Task.CompletedTask;
        }

        var temporaryBadgeExpiry =
            Convert.ToDateTime(context.User.FindFirst(
                                   c => c.Type == "TemporaryBadgeExpiry" &&
                                   c.Issuer == "https://contoso.com").Value);

        if (temporaryBadgeExpiry > DateTime.Now)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
