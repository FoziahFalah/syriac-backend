using SyriacSources.Backend.Application.Roles.Commands.CreateRole;
using SyriacSources.Backend.Application.Roles.Commands.DeleteRole;
using SyriacSources.Backend.Application.Roles.Commands.UpdateRole;
using SyriacSources.Backend.Application.Roles.Queries.GetRole;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Web.Endpoints;

public class Roles : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetRole)
            .MapPost(CreateRole)
            .MapPut(UpdateRole, "{id}")
            .MapDelete(DeleteRole, "{id}");
    }
    public async Task<ApplicationRole> GetRole(ISender sender, GetRoleCommand command)
    {
        return await sender.Send(command);
    }


    public async Task<int> CreateRole(ISender sender, CreateRoleCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateRole(ISender sender, int id, UpdateRoleCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }


    public async Task<IResult> DeleteRole(ISender sender, int id, DeleteRoleCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }



}
