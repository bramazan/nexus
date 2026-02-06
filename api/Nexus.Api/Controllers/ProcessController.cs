using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nexus.Application.GitLab.Commands;

namespace Nexus.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("pipelines")]
        public async Task<IResult> ProcessPipelines()
        {
            try
            {
                var count = await _mediator.Send(new ProcessPipelinesCommand());
                return Results.Ok(new { Message = "Pipelines normalization finished", ProcessedCount = count });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost("deployments")]
        public async Task<IResult> ProcessDeployments()
        {
            try
            {
                var count = await _mediator.Send(new ProcessDeploymentsCommand());
                return Results.Ok(new { Message = "Deployments normalization finished", ProcessedCount = count });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost("releases")]
        public async Task<IResult> ProcessReleases()
        {
            try
            {
                var count = await _mediator.Send(new ProcessReleasesCommand());
                return Results.Ok(new { Message = "Releases normalization finished", ProcessedCount = count });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost("issues")]
        public async Task<IResult> ProcessIssues()
        {
            try
            {
                var count = await _mediator.Send(new ProcessIssuesCommand());
                return Results.Ok(new { Message = "Issues normalization finished", ProcessedCount = count });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost("members")]
        public async Task<IResult> ProcessMembers()
        {
            try
            {
                var count = await _mediator.Send(new ProcessMembersCommand());
                return Results.Ok(new { Message = "Members normalization finished", ProcessedCount = count });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost("jobs")]
        public async Task<IResult> ProcessJobs()
        {
            try
            {
                var count = await _mediator.Send(new ProcessJobsCommand());
                return Results.Ok(new { Message = "Jobs normalization finished", ProcessedCount = count });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost("branches")]
        public async Task<IResult> ProcessBranches()
        {
            try
            {
                var count = await _mediator.Send(new ProcessBranchesCommand());
                return Results.Ok(new { Message = "Branches normalization finished", ProcessedCount = count });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost("approvals")]
        public async Task<IResult> ProcessApprovals()
        {
            try
            {
                var count = await _mediator.Send(new ProcessApprovalsCommand());
                return Results.Ok(new { Message = "Approvals normalization finished", ProcessedCount = count });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
