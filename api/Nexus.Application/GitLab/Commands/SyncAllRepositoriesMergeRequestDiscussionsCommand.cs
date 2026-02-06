using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncAllRepositoriesMergeRequestDiscussionsCommand : IRequest<int>;

    public class SyncAllRepositoriesMergeRequestDiscussionsCommandHandler : IRequestHandler<SyncAllRepositoriesMergeRequestDiscussionsCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public SyncAllRepositoriesMergeRequestDiscussionsCommandHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<int> Handle(SyncAllRepositoriesMergeRequestDiscussionsCommand request, CancellationToken cancellationToken)
        {
            // Get all PRs that are from GitLab (Repositories with Integration Type GitLab?)
            // For now, iterate all PRs where Repository has Integration.
            
            var pullRequests = await _context.PullRequests
                .Include(pr => pr.Repository)
                .Where(pr => pr.Repository.IntegrationId != Guid.Empty)
                .ToListAsync(cancellationToken);

            int count = 0;
            foreach (var pr in pullRequests)
            {
                // We need ProjectId (ExternalId of Repo) and MergeRequestIid (Number)
                if (int.TryParse(pr.Repository.ExternalId, out int projectId))
                {
                    try
                    {
                        await _mediator.Send(new SyncMergeRequestDiscussionsCommand(pr.Repository.IntegrationId, projectId, pr.Number), cancellationToken);
                        count++;
                    }
                    catch
                    {
                        // Log and continue
                    }
                }
            }

            return count;
        }
    }
}
