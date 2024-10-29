using SyriacSources.Backend.Application.Permissions.Commands.CreatePermission;
using SyriacSources.Backend.Application.Permissions.Commands.DeletePermission;
using SyriacSources.Backend.Application.Permissions.Commands.UpdatePermission;
using SyriacSources.Backend.Application.Permissions.Queries.GetPermissions;
using SyriacSources.Backend.Application.Permissions;
using SyriacSources.Backend.Application.Permissions.Queries;

namespace SyriacSources.Backend.Web.Endpoints;

public class Permissions: EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetPermissions, "Get")
            .MapPost(CreatePermission, "Create")
            .MapPut(UpdatePermission, "Update/{id}")
            .MapDelete(DeletePermission, "Delete/{id}");
    }

    public Task<List<PermissionDto>> GetPermissions(ISender sender) 
    { 
        return sender.Send(new GetPermissionsQuery());
    }

    public Task<int> CreatePermission(ISender sender, [AsParameters] CreatePermissionCommand command)
    {
        return sender.Send(command);
    }


    public async Task<IResult> UpdatePermission(ISender sender, int id, [AsParameters] UpdatePermissionCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeletePermission(ISender sender, int id, [AsParameters] DeletePermissionCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }
}
