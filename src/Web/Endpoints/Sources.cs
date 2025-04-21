using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using SyriacSources.Backend.Application.Sources;
using SyriacSources.Backend.Application.Sources.Commands;
using SyriacSources.Backend.Application.Sources.Commands.CreateSource;
using SyriacSources.Backend.Application.Sources.Commands.UpdateSource;
using SyriacSources.Backend.Application.Sources.Queries;
namespace SyriacSources.Backend.Web.Endpoints;
public class Sources : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateSource, "Create")
            .MapGet(GetSource, "Get/{id}")
            .MapGet(GetSources, "Get")
            .MapPut(UpdateSource, "Update/{id}")
            .MapDelete(DeleteSource, "Delete/{id}");
    }
    public async Task<IResult> CreateSource(ISender sender, CreateSourceCommand command)
    {
        var id = await sender.Send(command);
        return Results.Ok(id);
    }
    public Task<SourceDto> GetSource(ISender sender, int id)
    {
        return sender.Send(new GetSource(id));
    }
    public Task<List<SourceDto>> GetSources(ISender sender)
    {
        return sender.Send(new GetSourcesQuery());
    }
    public async Task<IResult> UpdateSource(ISender sender, int id, UpdateSource command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
    public async Task<IResult> DeleteSource(ISender sender, int id)
    {
        await sender.Send(new DeleteSource(id));
        return Results.NoContent();
    }
}
