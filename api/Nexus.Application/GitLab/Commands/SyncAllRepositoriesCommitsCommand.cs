using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncAllRepositoriesCommitsCommand : IRequest<Dictionary<string, int>>;

    public class SyncAllRepositoriesCommitsCommandHandler : IRequestHandler<SyncAllRepositoriesCommitsCommand, Dictionary<string, int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public SyncAllRepositoriesCommitsCommandHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Dictionary<string, int>> Handle(SyncAllRepositoriesCommitsCommand request, CancellationToken cancellationToken)
        {
            var repositories = await _context.Repositories.ToListAsync(cancellationToken);
            var results = new Dictionary<string, int>();

            foreach (var repo in repositories)
            {
                // Assuming ExternalId corresponds to ProjectId in GitLab
                if (!string.IsNullOrEmpty(repo.ExternalId))
                {
                    string key = repo.Name;
                    if (results.ContainsKey(key))
                    {
                        key = $"{repo.Name}-{repo.ExternalId}";
                    }

                    try 
                    {
                        var count = await _mediator.Send(new SyncGitLabRawDataCommand(repo.IntegrationId, repo.ExternalId, "commits"), cancellationToken);
                        results[key] = count;
                    }
                    catch (System.Exception)
                    {
                        // Log error or mark as failed in results but continue
                        results[key] = -1;
                    }
                }
            }

            return results;
        }
    }
}
