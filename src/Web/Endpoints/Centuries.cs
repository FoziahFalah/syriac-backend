using SyriacSources.Backend.Application.Centuries.Commands.CreateCentury;
using SyriacSources.Backend.Application.Centuries.Commands.DeleteCentury;
using SyriacSources.Backend.Application.Centuries.Commands.UpdateCentury;
using SyriacSources.Backend.Application.Centuries.Queries.GetCenturies;
using SyriacSources.Backend.Application.Centuries.Queries.GetCentury;
using SyriacSources.Backend.Application.Centuries;

namespace SyriacSources.Backend.Web.Endpoints;

public class Centuries : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateCentury, "Create")
            .MapGet(GetCentury, "Get/{id}")
            .MapGet(GetCenturies, "Get")
            .MapPut(UpdateCentury, "Update/{id}")
            .MapDelete(DeleteCentury, "Delete/{id}");
    }
    public Task<CenturyDto> GetCentury(ISender sender, int id) => sender.Send(new GetCenturyQuery(id));
    public Task<List<CenturyDto>> GetCenturies(ISender sender) => sender.Send(new GetCenturiesQuery());
    public Task<int> CreateCentury(ISender sender, CreateCenturyCommand command) => sender.Send(command);
    public async Task<IResult> UpdateCentury(ISender sender, int id, UpdateCenturyCommand command)
    {
        if (id != command.Id)
            return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
    public async Task<IResult> DeleteCentury(ISender sender, int id)
    {
        await sender.Send(new DeleteCenturyCommand(id));
        return Results.NoContent();
    }
}
