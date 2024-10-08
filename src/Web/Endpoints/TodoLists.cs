using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using SyriacSources.Backend.Application.TodoLists.Commands.CreateTodoList;
using SyriacSources.Backend.Application.TodoLists.Commands.DeleteTodoList;
using SyriacSources.Backend.Application.TodoLists.Commands.UpdateTodoList;
using SyriacSources.Backend.Application.TodoLists.Queries.GetTodos;

namespace SyriacSources.Backend.Web.Endpoints;

[Authorize(Policy = "todolist")]
public class TodoLists : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetTodoLists)
            .MapPost(CreateTodoList)
            .MapPut(UpdateTodoList, "{id}")
            .MapDelete(DeleteTodoList, "{id}");
    }

    [Authorize(Policy = "todolist:getall")]
    public Task<TodosVm> GetTodoLists(ISender sender)
    {
        return  sender.Send(new GetTodosQuery());
    }

    [Authorize(Policy = "todolist:create")]
    public Task<int> CreateTodoList(ISender sender, CreateTodoListCommand command)
    {
        return sender.Send(command);
    }

    [Authorize(Policy = "todolist:update")]
    public async Task<IResult> UpdateTodoList(ISender sender, int id, UpdateTodoListCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    [Authorize(Policy = "todolist:delete")]
    public async Task<IResult> DeleteTodoList(ISender sender, int id)
    {
        await sender.Send(new DeleteTodoListCommand(id));
        return Results.NoContent();
    }
}
