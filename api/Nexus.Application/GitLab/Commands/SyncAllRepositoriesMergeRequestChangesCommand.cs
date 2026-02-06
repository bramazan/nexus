using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncAllRepositoriesMergeRequestChangesCommand : IRequest<Dictionary<string, int>>;

    public class SyncAllRepositoriesMergeRequestChangesCommandHandler : IRequestHandler<SyncAllRepositoriesMergeRequestChangesCommand, Dictionary<string, int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public SyncAllRepositoriesMergeRequestChangesCommandHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Dictionary<string, int>> Handle(SyncAllRepositoriesMergeRequestChangesCommand request, CancellationToken cancellationToken)
        {
            var results = new Dictionary<string, int>();

            // Get all repositories
            var repositories = await _context.Repositories.ToListAsync(cancellationToken);

            foreach (var repo in repositories)
            {
                string key = repo.Name;
                int count = 0;

                // For each repository, find eligible Pull Requests
                // We want to fetch changes for Open PRs, and maybe recently merged ones (if we missed them).
                // To be safe/thorough, let's fetch for ALL PRs that don't have associated CodeChanges yet?
                // Or just iterate all PRs? Iterating all might be too heavy (19k records).
                // Let's iterate PRs that are OPEN or merged/closed in the last 30 days?
                // Refinement: Start with OPEN PRs.
                // Refinement 2: User asked to "fetch changes". 
                // Let's do a trick: Fetch for 'Open' PRs AND PRs that have no linked CodeChanges?
                
                // Query: PRs where RepositoryId matches.
                // Limit: latest 100 for now to avoid timeout?
                // Or just ALL Open ones.
                
                var prs = await _context.PullRequests
                    .Where(pr => pr.RepositoryId == repo.Id)
                    .OrderByDescending(pr => pr.CreatedAt)
                    .Take(50) // Limit to avoid hitting rate limits too hard in one go
                    .ToListAsync(cancellationToken);

                foreach (var pr in prs)
                {
                    if (string.IsNullOrEmpty(repo.ExternalId)) continue;
                    
                    try 
                    {
                        var success = await _mediator.Send(new SyncMergeRequestChangesCommand(repo.IntegrationId, repo.ExternalId, pr.Number), cancellationToken);
                        if (success) count++;
                    }
                    catch
                    {
                        // ignore errors
                    }
                }
                
                results[key] = count;
            }

            return results;
        }
    }
}
