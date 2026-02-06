using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncAllRepositoriesPipelinesCommand : IRequest<Dictionary<string, int>>;

    public class SyncAllRepositoriesPipelinesCommandHandler : IRequestHandler<SyncAllRepositoriesPipelinesCommand, Dictionary<string, int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public SyncAllRepositoriesPipelinesCommandHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Dictionary<string, int>> Handle(SyncAllRepositoriesPipelinesCommand request, CancellationToken cancellationToken)
        {
            var repositories = await _context.Repositories.ToListAsync(cancellationToken);
            var results = new Dictionary<string, int>();

            foreach (var repo in repositories)
            {
                if (!string.IsNullOrEmpty(repo.ExternalId) && int.TryParse(repo.ExternalId, out int projectId))
                {
                    string key = repo.Name;
                    if (results.ContainsKey(key))
                    {
                        key = $"{repo.Name}-{repo.ExternalId}";
                    }

                    try 
                    {
                        var count = await _mediator.Send(new SyncPipelinesCommand(repo.IntegrationId, projectId), cancellationToken);
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
