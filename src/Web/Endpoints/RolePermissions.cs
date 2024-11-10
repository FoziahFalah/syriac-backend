using SyriacSources.Backend.Application.ApplicationRolePermissions.Commands.UpdateRolePermission;
using SyriacSources.Backend.Application.RolePermissions.Queries.GetRolePermissions;
using SyriacSources.Backend.Domain.Constants;

namespace SyriacSources.Backend.Web.Endpoints;


[Authorize]
public class RolePermissions: EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetRolePermissions, "Get")
            .MapPut(UpdateRolePermissions, "Update");
    }

    public Task<RolePermissionVm> GetRolePermissions(ISender sender, [AsParameters]  GetRolePermissionsQuery query) 
    { 
        return sender.Send(query);
    }

    public async Task<IResult> UpdateRolePermissions(ISender sender, [AsParameters] UpdateRolePermissionCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }
}
