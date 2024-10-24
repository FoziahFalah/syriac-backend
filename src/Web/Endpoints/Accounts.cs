using Microsoft.AspNetCore.Mvc;
using SyriacSources.Backend.Application.Account.Commands.Login;
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
            .MapPost(Login, "Login");
    }


    public async Task<LoginResponseDto> Login(ISender sender, LoginCommand command)
    {
        return await sender.Send(command);
    }

}
