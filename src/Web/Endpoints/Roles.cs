using Microsoft.AspNetCore.Authorization;
using SyriacSources.Backend.Application.Roles;
using SyriacSources.Backend.Application.Roles.Commands.CreateRole;
using SyriacSources.Backend.Application.Roles.Commands.DeleteRole;
using SyriacSources.Backend.Application.Roles.Commands.UpdateRole;
using SyriacSources.Backend.Application.Roles.Queries.GetRole;

namespace SyriacSources.Backend.Web.Endpoints;

[Authorize(Policy = "roles")]
public class Roles : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateRole ,"Create")
            .MapGet(GetRole, "Get")
            .MapPut(UpdateRole, "Update/{id}")
            .MapDelete(DeleteRole, "Delete/{id}");
    }

    [Authorize(Policy ="roles:getrole")]
    public Task<ApplicationRoleDto> GetRole(ISender sender, int id)
    {
        return sender.Send(new GetRoleQuery(id));
    }

    [Authorize(Policy = "roles:createrole")]
    public Task<int> CreateRole(ISender sender,CreateRoleCommand command)
    {
        return sender.Send(command);
    }

    [Authorize(Policy = "roles:updaterole")]
    public async Task<IResult> UpdateRole(ISender sender, int id, UpdateRoleCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    [Authorize(Policy = "roles:deleterole")]
    public async Task<IResult> DeleteRole(ISender sender, int id)
    {
        await sender.Send(new DeleteRoleCommand(id));
        return Results.NoContent();
    }
}
