using SyriacSources.Backend.Application.Roles;
using SyriacSources.Backend.Application.Roles.Commands.CreateRole;
using SyriacSources.Backend.Application.Roles.Commands.DeleteRole;
using SyriacSources.Backend.Application.Roles.Commands.UpdateRole;
using SyriacSources.Backend.Application.Roles.Queries.GetRole;
using SyriacSources.Backend.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SyriacSources.Backend.Web.Endpoints;

public class Roles : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateRole)
            .MapGet(GetRole)
            .MapPut(UpdateRole, "{id}")
            .MapDelete(DeleteRole, "{id}");
    }

    public Task<ApplicationRoleDto> GetRole(ISender sender, int id)
    {
        return sender.Send(new GetRoleQuery(id));
    }

    public Task<int> CreateRole(ISender sender, [AsParameters] CreateRoleCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateRole(ISender sender, int id, [AsParameters] UpdateRoleCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }


    public async Task<IResult> DeleteRole(ISender sender, int id)
    {
        await sender.Send(new DeleteRoleCommand(id));
        return Results.NoContent();
    }
}
