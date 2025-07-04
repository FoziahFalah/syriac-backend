﻿using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Sources;
using SyriacSources.Backend.Application.Sources.Commands;
using SyriacSources.Backend.Application.Sources.Commands.CreateSource;
using SyriacSources.Backend.Application.Sources.Commands.DeleteFiles;
using SyriacSources.Backend.Application.Sources.Commands.UpdateSource;
using SyriacSources.Backend.Application.Sources.Queries;
using SyriacSources.Backend.Application.Sources.Queries.SearchSources;

namespace SyriacSources.Backend.Web.Endpoints;
public class Sources : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this).DisableAntiforgery()
            .MapPost(CreateSource, "Create")
            .MapGet(GetSource, "Get/{id}")
            .MapGet(GetSources, "Get")
            .MapPut(UpdateSource, "Update/{id}")
            .MapDelete(DeleteSource, "Delete/{id}")
            .MapPost(SearchSources, "Search")
            .MapPost(UploadSourceCover, "UploadSourceCoverPhoto")
            .MapDelete(DeleteAttachment, "DeleteAttachment/{id}")
            .MapDelete(DeleteCoverPhoto, "DeleteCoverPhoto/{sourceId}");

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
    public async Task<IResult> UpdateSource(ISender sender, int id, UpdateSourceCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
    public async Task<IResult> DeleteSource(ISender sender, int id)
    {
        await sender.Send(new DeleteSourceCommand(id));
        return Results.NoContent();
    }

    [Consumes("multipart/form-data")]
    public async Task<IResult> UploadSourceCover(ISender sender, [FromForm] UploadCoverPhotoCommand command)
    {
        var result = await sender.Send(command);
        return Results.Ok(result);
    }

    public async Task<IResult> SearchSources([FromBody] SearchSources query, ISender sender)
    {
        var result = await sender.Send(query);
        return Results.Ok(result);
    }
    public async Task<IResult> DeleteAttachment(ISender sender, int id)
    {
        await sender.Send(new DeleteAttachmentCommand(id));
        return Results.NoContent();
    }
    public async Task<IResult> DeleteCoverPhoto(ISender sender, int sourceId)
    {
        await sender.Send(new DeleteCoverPhotoCommand(sourceId));
        return Results.NoContent();
    }
}







