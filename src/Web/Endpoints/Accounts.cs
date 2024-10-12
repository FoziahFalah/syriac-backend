using Microsoft.AspNetCore.Mvc;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.TodoItems.Commands.CreateTodoItem;
using SyriacSources.Backend.Application.TodoItems.Commands.DeleteTodoItem;
using SyriacSources.Backend.Application.TodoItems.Commands.UpdateTodoItem;
using SyriacSources.Backend.Application.TodoItems.Commands.UpdateTodoItemDetail;
using SyriacSources.Backend.Application.TodoItems.Queries.GetTodoItemsWithPagination;

namespace SyriacSources.Backend.Web.Endpoints;

public class Accounts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateTodoItem);
    }

    public Task<int> CreateTodoItem(ISender sender, CreateTodoItemCommand command)
    {
        return sender.Send(command);
    }
    public Task<IResult> Login(ISender sender, CreateTodoItemCommand command)
    {
        return sender.Send(command);
    }

}
