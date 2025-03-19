using MediatR;
using SyriacSources.Backend.Application.Authors;
using SyriacSources.Backend.Application.Authors.Commands.CreateAuthor;
using SyriacSources.Backend.Application.Authors.Commands.DeleteAuthor;
using SyriacSources.Backend.Application.Authors.Commands.UpdateAuthor;
using SyriacSources.Backend.Application.Authors.Queries.GetAuthor;
using SyriacSources.Backend.Application.Authors.Queries.GetAuthors;
namespace SyriacSources.Backend.Web.Endpoints;
public class Authors : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateAuthor, "Create")
            .MapGet(GetAuthor, "Get/{id}")
            .MapGet(GetAuthors, "Get")
            .MapPut(UpdateAuthor, "Update/{id}")
            .MapDelete(DeleteAuthor, "Delete/{id}");
    }
    // :white_check_mark: Get Single Author by ID
    public Task<AuthorDto> GetAuthor(ISender sender, int id)
    {
        return sender.Send(new GetAuthorQuery(id));
    }
    // :white_check_mark: Get All Authors
    public Task<List<AuthorDto>> GetAuthors(ISender sender)
    {
        return sender.Send(new GetAuthorsQuery());
    }
    // :white_check_mark: Create Author
    public Task<int> CreateAuthor(ISender sender, CreateAuthorCommand command)
    {
        return sender.Send(command);
    }
    // :white_check_mark: Update Author
    public async Task<IResult> UpdateAuthor(ISender sender, int id, UpdateAuthorCommand command)
    {
        if (id != command.Id)
            return Results.BadRequest("المعرف لا يتطابق مع معرف الطلب.");
        await sender.Send(command);
        return Results.NoContent();
    }
    // :white_check_mark: Delete Author
    public async Task<IResult> DeleteAuthor(ISender sender, int id)
    {
        await sender.Send(new DeleteAuthorCommand(id));
        return Results.NoContent();
    }
}
