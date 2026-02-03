using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncGitLabProjectDataCommand(Guid IntegrationId, string ProjectId) : IRequest<Dictionary<string, int>>;

    public class SyncGitLabProjectDataCommandHandler : IRequestHandler<SyncGitLabProjectDataCommand, Dictionary<string, int>>
    {
        private readonly IMediator _mediator;

        public SyncGitLabProjectDataCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Dictionary<string, int>> Handle(SyncGitLabProjectDataCommand request, CancellationToken cancellationToken)
        {
            var dataTypes = new[] 
            { 
                "commits", 
                "mergerequests", 
                "pipelines", 
                "jobs", 
                "issues", 
                "deployments", 
                "releases" 
            };

            var results = new Dictionary<string, int>();

            foreach (var type in dataTypes)
            {
                // We sequentially call the existing command which handles pagination and deduplication
                var count = await _mediator.Send(new SyncGitLabRawDataCommand(request.IntegrationId, request.ProjectId, type), cancellationToken);
                results.Add(type, count);
            }

            return results;
        }
    }
}
