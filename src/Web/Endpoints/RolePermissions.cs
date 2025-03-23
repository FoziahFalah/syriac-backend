using SyriacSources.Backend.Application.ApplicationRolePermissions.Commands.UpdateRolePermission;
using SyriacSources.Backend.Domain.Constants;

namespace SyriacSources.Backend.Web.Endpoints;

public class RolePermissions: EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPut(UpdateRolePermissions, "Update/");
    }

    public async Task<IResult> UpdateRolePermissions(ISender sender, UpdateRolePermissionCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }
}
