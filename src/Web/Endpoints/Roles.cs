using Microsoft.AspNetCore.Authorization;
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
            .MapPost(CreateRole ,"Create")
            .MapGet(GetRole, "Get")
            .MapPut(UpdateRole, "Update/{id}")
            .MapDelete(DeleteRole, "Delete/{id}");
    }

    [Authorize(Roles = "Administrator")]
    public Task<ApplicationRoleDto> GetRole(ISender sender, int id)
    {
        return sender.Send(new GetRoleQuery(id));
    }

    [Authorize(Roles = "Administrator")]
    public Task<int> CreateRole(ISender sender, [AsParameters] CreateRoleCommand command)
    {
        return sender.Send(command);
    }

    [Authorize(Roles = "Administrator")]

    public async Task<IResult> UpdateRole(ISender sender, int id, [AsParameters] UpdateRoleCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    [Authorize(Roles = "Administrator")]
    public async Task<IResult> DeleteRole(ISender sender, int id, [AsParameters]  DeleteRoleCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }
}
