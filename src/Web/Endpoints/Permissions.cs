using SyriacSources.Backend.Application.Permissions.Commands.CreatePermission;
using SyriacSources.Backend.Application.Permissions.Commands.DeletePermission;
using SyriacSources.Backend.Application.Permissions.Commands.UpdatePermission;
using SyriacSources.Backend.Application.Permissions.Queries.GetPermissions;
using SyriacSources.Backend.Application.Permissions.Queries.GetPermissionsByRoleId;

namespace SyriacSources.Backend.Web.Endpoints;

public class Permissions: EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .AllowAnonymous()
            .MapGet(GetPermissions, "Get")
            .MapGet(GetPermissionIdsByRoleId, "GetPermissionIdsByRoleId/{roleId}")
            .MapPost(CreatePermission, "Create")
            .MapPut(UpdatePermission, "Update/{id}")
            .MapDelete(DeletePermission, "Delete/{id}");
    }

    public Task<List<PermissionTreeNodeDto>> GetPermissions(ISender sender) 
    { 
        return sender.Send(new GetPermissionsQuery());
    }
    public Task<List<int>> GetPermissionIdsByRoleId(ISender sender, int roleId)
    {
        return sender.Send(new GetPermissionsByRoleIdQuery(roleId));
    }

    public Task<int> CreatePermission(ISender sender, [AsParameters] CreatePermissionCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdatePermission(ISender sender, int id, UpdatePermissionCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.Ok();
    }

    public async Task<IResult> DeletePermission(ISender sender, [AsParameters] DeletePermissionCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }
}
