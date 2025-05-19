using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Sources;
using SyriacSources.Backend.Application.Sources.Commands;
using SyriacSources.Backend.Application.Sources.Commands.CreateSource;
using SyriacSources.Backend.Application.Sources.Commands.UpdateSource;
using SyriacSources.Backend.Application.Sources.Queries;
using SyriacSources.Backend.Application.Sources.Queries.SearchSources;
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
        app.MapPost("/api/Sources/UploadCover", async (HttpRequest request) =>
        {
            var file = request.Form.Files["file"];
            if (file == null || file.Length == 0)
                return Results.BadRequest("الملف غير موجود");
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
            var fileName = $"PHOTO-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}-{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Results.Ok(new
            {
                FileName = fileName,
                FilePath = $"/uploads/{fileName}",  
                FileExtension = Path.GetExtension(file.FileName)
            });
        })
 .Accepts<IFormFile>("multipart/form-data")
 .Produces(200)
 .WithName("UploadCoverPhoto");

        app.MapPost("/api/sources/search", async (
    [FromBody] SearchSources query,
    ISender sender) =>
        {
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
.WithName("SearchSources")
.Produces<List<SourceDto>>(200);


    }
    public async Task<IResult> CreateSource(ISender sender, CreateSource command)
    {
        var id = await sender.Send(command);
        return Results.Ok(id);
    }
    public Task<SourceDto> GetSource(ISender sender, int id)
    {
        return sender.Send(new GetSource(id));
    }
    public async Task<IResult> GetSources(ISender sender, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await sender.Send(new GetSourcesWithPagination
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        });
        return Results.Ok(result);
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

