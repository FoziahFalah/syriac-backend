using SyriacSources.Backend.Application.Users.Commands.CreateUser;
using SyriacSources.Backend.Application.Users.Commands.DeleteUser;
using SyriacSources.Backend.Application.Users.Commands.UpdateUser;
using SyriacSources.Backend.Application.Users.Queries.GetUser;
using SyriacSources.Backend.Application.Users;
using SyriacSources.Backend.Application.User;
using SyriacSources.Backend.Application.Users.Commands.CreateUser;

namespace SyriacSources.Backend.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateUser, "Create")
            .MapGet(GetUser, "Get")
            .MapPut(UpdateUser, "Update/{id}")
            .MapDelete(DeleteUser, "Delete/{id}");
    }


    public Task<ApplicationUserDto> GetUser(ISender sender, int id)
    {
        return sender.Send(new GetUser(id));
    }


    public Task<Result> CreateUser(ISender sender, CreateUserCommand command)
    {
        return sender.Send(command);
    }


    public async Task<IResult> UpdateUser(ISender sender, int id, UpdateUserCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteUser(ISender sender, int id)
    {
        await sender.Send(new DeleteUserCommand(id));
        return Results.NoContent();
    }
}
