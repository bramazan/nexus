using MediatR;
using Nexus.Application.Common.Interfaces;
using Nexus.Application.Common.Models;
using Nexus.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncGitLabGroupCommand(Guid IntegrationId, string GroupId) : IRequest<int>;

    public class SyncGitLabGroupCommandHandler : IRequestHandler<SyncGitLabGroupCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGitLabConnector _connector;

        public SyncGitLabGroupCommandHandler(IApplicationDbContext context, IGitLabConnector connector)
        {
            _context = context;
            _connector = connector;
        }

        public async Task<int> Handle(SyncGitLabGroupCommand request, CancellationToken cancellationToken)
        {
            var projects = await _connector.GetProjectsAsync(request.IntegrationId, request.GroupId);
            int syncedCount = 0;

            foreach (var project in projects)
            {
                var existingRepo = await _context.Repositories
                    .FirstOrDefaultAsync(r => r.ExternalId == project.Id.ToString() && r.IntegrationId == request.IntegrationId, cancellationToken);

                if (existingRepo == null)
                {
                    var newRepo = new Repository
                    {
                        IntegrationId = request.IntegrationId,
                        Name = project.Name,
                        ExternalId = project.Id.ToString(),
                        Url = project.WebUrl,
                        DefaultBranch = project.DefaultBranch
                    };
                    _context.Repositories.Add(newRepo);
                }
                else
                {
                    existingRepo.Name = project.Name;
                    existingRepo.Url = project.WebUrl;
                    existingRepo.DefaultBranch = project.DefaultBranch;
                }
                syncedCount++;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return syncedCount;
        }
    }
}
