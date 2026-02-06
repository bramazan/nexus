using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nexus.Application.Common.Interfaces;
using Nexus.Application.GitLab.Commands;
using Nexus.Application.Jira.Commands;
using Nexus.Application.Instana.Commands;

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

    [HttpPost("gitlab/all-commits")]
    public async Task<IResult> SyncAllRepositoriesCommits()
    {
        try
        {
            var results = await _mediator.Send(new SyncAllRepositoriesCommitsCommand());
            return Results.Ok(new { Message = "All repositories commits sync finished", Results = results });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("gitlab/all-merge-requests")]
    public async Task<IResult> SyncAllRepositoriesMergeRequests()
    {
        try
        {
            var results = await _mediator.Send(new SyncAllRepositoriesMergeRequestsCommand());
            return Results.Ok(new { Message = "All repositories merge requests sync finished", Results = results });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("gitlab/all-mr-changes")]
    public async Task<IResult> SyncAllRepositoriesMergeRequestChanges()
    {
        try
        {
            var results = await _mediator.Send(new SyncAllRepositoriesMergeRequestChangesCommand());
            return Results.Ok(new { Message = "All repositories MR changes sync finished", Results = results });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("gitlab/all-mr-discussions")]
    public async Task<IResult> SyncAllRepositoriesMergeRequestDiscussions()
    {
        try
        {
            var count = await _mediator.Send(new SyncAllRepositoriesMergeRequestDiscussionsCommand());
            return Results.Ok(new { Message = "All repositories MR discussions sync finished", SyncedCount = count });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("gitlab/all-mr-approvals")]
    public async Task<IResult> SyncAllRepositoriesMergeRequestApprovals()
    {
        try
        {
            var count = await _mediator.Send(new SyncAllRepositoriesMergeRequestApprovalsCommand());
            return Results.Ok(new { Message = "All repositories MR approvals sync finished", SyncedCount = count });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("gitlab/all-pipelines")]
    public async Task<IResult> SyncAllRepositoriesPipelines()
    {
        try
        {
            var results = await _mediator.Send(new SyncAllRepositoriesPipelinesCommand());
            return Results.Ok(new { Message = "All repositories pipelines sync finished", Results = results });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("gitlab/all-deployments")]
    public async Task<IResult> SyncAllRepositoriesDeployments()
    {
        try
        {
            var results = await _mediator.Send(new SyncAllRepositoriesDeploymentsCommand());
            return Results.Ok(new { Message = "All repositories deployments sync finished", Results = results });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("gitlab/all-releases")]
    public async Task<IResult> SyncAllRepositoriesReleases()
    {
        try
        {
            var results = await _mediator.Send(new SyncAllRepositoriesReleasesCommand());
            return Results.Ok(new { Message = "All repositories releases sync finished", Results = results });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
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

    [HttpPost("jira/{integrationId}/issues")]
    public async Task<IResult> SyncJiraIssues(Guid integrationId)
    {
        try
        {
            var count = await _mediator.Send(new SyncJiraIssuesCommand(integrationId));
            return Results.Ok(new { Message = "Jira Issues sync finished", Count = count });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("instana/{integrationId}/events")]
    public async Task<IResult> SyncInstanaEvents(Guid integrationId)
    {
        try
        {
            var count = await _mediator.Send(new SyncInstanaEventsCommand(integrationId));
            return Results.Ok(new { Message = "Instana Events sync finished", Count = count });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("instana/{integrationId}/metrics")]
    public async Task<IResult> SyncInstanaMetrics(Guid integrationId)
    {
        try
        {
            var count = await _mediator.Send(new SyncInstanaMetricsCommand(integrationId));
            return Results.Ok(new { Message = "Instana Metrics sync finished", Count = count });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("gitlab/all-issues")]
    public async Task<IResult> SyncAllRepositoriesIssues()
    {
        try
        {
            var results = await _mediator.Send(new SyncAllRepositoriesGenericCommand("issues"));
            return Results.Ok(new { Message = "All repositories issues sync finished", Results = results });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("gitlab/all-members")]
    public async Task<IResult> SyncAllRepositoriesMembers()
    {
        try
        {
            var results = await _mediator.Send(new SyncAllRepositoriesGenericCommand("members"));
            return Results.Ok(new { Message = "All repositories members sync finished", Results = results });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("gitlab/all-branches")]
    public async Task<IResult> SyncAllRepositoriesBranches()
    {
        try
        {
            var results = await _mediator.Send(new SyncAllRepositoriesGenericCommand("branches"));
            return Results.Ok(new { Message = "All repositories branches sync finished", Results = results });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("gitlab/all-jobs")]
    public async Task<IResult> SyncAllRepositoriesJobs()
    {
        try
        {
            var results = await _mediator.Send(new SyncAllRepositoriesGenericCommand("jobs"));
            return Results.Ok(new { Message = "All repositories jobs sync finished", Results = results });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
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

