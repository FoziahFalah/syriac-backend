using SyriacSources.Backend.Application.DateFormats.Commands.CreateDateFormat;
using SyriacSources.Backend.Application.DateFormats.Commands.DeleteDateFormat;
using SyriacSources.Backend.Application.DateFormats.Commands.UpdateDateFormat;
using SyriacSources.Backend.Application.DateFormats.Queries.GetDateFormat;
using SyriacSources.Backend.Application.DateFormats.Queries.GetDateFormats;
using SyriacSources.Backend.Application.DateFormats;

public class DateFormats : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateDateFormat, "Create")
            .MapGet(GetDateFormat, "Get/{id}")
            .MapGet(GetDateFormats, "Get")
            .MapPut(UpdateDateFormat, "Update/{id}")
            .MapDelete(DeleteDateFormat, "Delete/{id}");
    }
    public Task<DateFormatDto> GetDateFormat(ISender sender, int id) => sender.Send(new GetDateFormatQuery(id));
    public Task<List<DateFormatDto>> GetDateFormats(ISender sender) => sender.Send(new GetDateFormatsQuery());
    public Task<int> CreateDateFormat(ISender sender, CreateDateFormatCommand command) => sender.Send(command);
    public async Task<IResult> UpdateDateFormat(ISender sender, int id, UpdateDateFormatCommand command) { await sender.Send(command); return Results.NoContent(); }
    public async Task<IResult> DeleteDateFormat(ISender sender, int id) { await sender.Send(new DeleteDateFormatCommand(id)); return Results.NoContent(); }
}
