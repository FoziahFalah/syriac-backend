using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using SyriacSources.Backend.Application.Excerpts.Commands.CreateExcerpt;
using SyriacSources.Backend.Application.Excerpts.Commands.DeleteExcerpt;
using SyriacSources.Backend.Application.Excerpts.Commands.UpdateExcerpt;
using SyriacSources.Backend.Application.Excerpts.Dots;
using SyriacSources.Backend.Application.Excerpts.Queries.GetExcerptById;
using SyriacSources.Backend.Application.Excerpts.Queries.GetExcerpts;
namespace SyriacSources.Backend.Web.Endpoints;
public class Excerpts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .MapPost(CreateExcerpt, "Create")
           .MapGet(GetAllExcerpts, "GetAll")
           .MapGet(GetExcerptById, "Get/{id}")
           .MapPut(UpdateExcerpt, "Update/{id}")
           .MapDelete(DeleteExcerpt, "Delete/{id}");
    }
    public async Task<Ok<int>> CreateExcerpt(ISender sender, CreateExcerpt command)
    {
        var id = await sender.Send(command);
        return TypedResults.Ok(id);
    }
    public async Task<Ok<List<ExcerptDto>>> GetAllExcerpts(ISender sender)
    {
        var result = await sender.Send(new GetExcerpts());
        return TypedResults.Ok(result);
    }
    public async Task<Results<Ok<ExcerptDto>, NotFound>> GetExcerptById(ISender sender, int id)
    {
        var result = await sender.Send(new GetExcerptById { Id = id });
        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    public async Task<Results<Ok<int>, NotFound>> UpdateExcerpt(ISender sender, int id, UpdateExcerpt command)
    {
        if (id != command.Id)
            return TypedResults.NotFound();
        var result = await sender.Send(command);
        return TypedResults.Ok(result);
    }

    public async Task<Results<Ok<int>, NotFound>> DeleteExcerpt(ISender sender, int id)
    {
        var result = await sender.Send(new DeleteExcerpt { Id = id });
        return TypedResults.Ok(result);
    }
}
