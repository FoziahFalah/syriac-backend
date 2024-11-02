
using SyriacSources.Backend.Application.ApplicationRolePermissions.Commands.UpdateRolePermission;
using SyriacSources.Backend.Application.RolePermissions.Queries.GetRolePermissions;

namespace SyriacSources.Backend.Web.Endpoints;

public class RolePermissions: EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetRolePermissions, "Get")
            .MapPut(UpdateRolePermissions, "Update");
    }

    public Task<List<RolePermissionDto>> GetRolePermissions(ISender sender, [AsParameters]  GetRolePermissionsQuery query) 
    { 
        return sender.Send(query);
    }

    public async Task<IResult> UpdateRolePermissions(ISender sender, [AsParameters] UpdateRolePermissionCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }
}
