using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncAllRepositoriesMergeRequestApprovalsCommand : IRequest<int>;

    public class SyncAllRepositoriesMergeRequestApprovalsCommandHandler : IRequestHandler<SyncAllRepositoriesMergeRequestApprovalsCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public SyncAllRepositoriesMergeRequestApprovalsCommandHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<int> Handle(SyncAllRepositoriesMergeRequestApprovalsCommand request, CancellationToken cancellationToken)
        {
            var pullRequests = await _context.PullRequests
                .Include(pr => pr.Repository)
                .Where(pr => pr.Repository.IntegrationId != Guid.Empty)
                .ToListAsync(cancellationToken);

            int count = 0;
            foreach (var pr in pullRequests)
            {
                if (int.TryParse(pr.Repository.ExternalId, out int projectId))
                {
                    try
                    {
                        await _mediator.Send(new SyncMergeRequestApprovalsCommand(pr.Repository.IntegrationId, projectId, pr.Number), cancellationToken);
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
