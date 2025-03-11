using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.Languages;
using SyriacSources.Backend.Application.Languages.Commands.CreateLanguage;
using SyriacSources.Backend.Application.Languages.Commands.DeleteLanguage;
using SyriacSources.Backend.Application.Languages.Commands.UpdateLanguage;
using SyriacSources.Backend.Application.Languages.Queries.GetLanguage;
using SyriacSources.Backend.Application.Languages.Queries.GetLanguages;
namespace SyriacSources.Backend.Web.Endpoints;
public class Languages : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateLanguage, "Create")
            .MapGet(GetLanguage, "Get/{id}")
            .MapGet(GetLanguages, "Get")
            .MapPut(UpdateLanguage, "Update/{id}")
            .MapDelete(DeleteLanguage, "Delete/{id}");
    }
    public Task<LanguageDto> GetLanguage(ISender sender, int id) => sender.Send(new GetLanguageQuery(id));
    public Task<List<LanguageDto>> GetLanguages(ISender sender) => sender.Send(new GetLanguagesQuery());
    public Task<int> CreateLanguage(ISender sender, CreateLanguageCommand command) => sender.Send(command);
    public async Task<IResult> UpdateLanguage(ISender sender, int id, UpdateLanguageCommand command)
    {
        if (id != command.Id)
            return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
    public async Task<IResult> DeleteLanguage(ISender sender, int id)
    {
        await sender.Send(new DeleteLanguageCommand(id));
        return Results.NoContent();
    }
}








