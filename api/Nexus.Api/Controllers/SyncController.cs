using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nexus.Application.Common.Interfaces;
using Nexus.Application.GitLab.Commands;

namespace Nexus.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SyncController : ControllerBase
{
    private readonly IMediator _mediator;

    public SyncController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("gitlab/{integrationId}")]
    public async Task<IResult> SyncGitLab(Guid integrationId, [FromServices] IGitLabConnector connector)
    {
        try
        {
            await connector.SyncProjectsAsync(integrationId);
            return Results.Ok("Sync started successfully.");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("gitlab/{integrationId}/groups/{groupId}/init")]
    public async Task<IResult> SyncGitLabGroup(Guid integrationId, string groupId)
    {
        var count = await _mediator.Send(new SyncGitLabGroupCommand(integrationId, groupId));
        return Results.Ok(new { Message = "Sync started", ProjectsSynced = count });
    }

    [HttpPost("gitlab/{integrationId}/projects/{projectId}/{dataType}")]
    public async Task<IResult> SyncGitLabRawData(Guid integrationId, string projectId, string dataType)
    {
        try
        {
            var count = await _mediator.Send(new SyncGitLabRawDataCommand(integrationId, projectId, dataType));
            return Results.Ok(new { Message = "Sync finished", ItemsSynced = count });
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    [HttpPost("gitlab/{integrationId}/projects/{projectId}/all")]
    public async Task<IResult> SyncGitLabProjectAll(Guid integrationId, string projectId)
    {
        try
        {
            var results = await _mediator.Send(new SyncGitLabProjectDataCommand(integrationId, projectId));
            return Results.Ok(new { Message = "Project sync finished", Results = results });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
