using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncAllRepositoriesMergeRequestsCommand : IRequest<Dictionary<string, int>>;

    public class SyncAllRepositoriesMergeRequestsCommandHandler : IRequestHandler<SyncAllRepositoriesMergeRequestsCommand, Dictionary<string, int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public SyncAllRepositoriesMergeRequestsCommandHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Dictionary<string, int>> Handle(SyncAllRepositoriesMergeRequestsCommand request, CancellationToken cancellationToken)
        {
            var repositories = await _context.Repositories.ToListAsync(cancellationToken);
            var results = new Dictionary<string, int>();

            foreach (var repo in repositories)
            {
                if (!string.IsNullOrEmpty(repo.ExternalId))
                {
                    string key = repo.Name;
                    if (results.ContainsKey(key))
                    {
                        key = $"{repo.Name}-{repo.ExternalId}";
                    }

                    try 
                    {
                        // Use "pull_request" to align with raw_data.md requirements
                        var count = await _mediator.Send(new SyncGitLabRawDataCommand(repo.IntegrationId, repo.ExternalId, "pull_request"), cancellationToken);
                        results[key] = count;
                    }
                    catch (System.Exception)
                    {
                        results[key] = -1;
                    }
                }
            }

            return results;
        }
    }
}
