using SyriacSources.Backend.Application.ApplicationRolePermissions.Commands.UpdateRolePermission;
using SyriacSources.Backend.Domain.Constants;

namespace SyriacSources.Backend.Web.Endpoints;


[Authorize]
public class RolePermissions: EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPut(UpdateRolePermissions, "Update/{roleId}");
    }

    public async Task<IResult> UpdateRolePermissions(ISender sender, [AsParameters] UpdateRolePermissionCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }
}
