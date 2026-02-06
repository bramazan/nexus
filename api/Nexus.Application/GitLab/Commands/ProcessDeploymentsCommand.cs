using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Application.Common.Models;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Application.GitLab.Commands
{
    public record ProcessDeploymentsCommand : IRequest<int>;

    public class ProcessDeploymentsCommandHandler : IRequestHandler<ProcessDeploymentsCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public ProcessDeploymentsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(ProcessDeploymentsCommand request, CancellationToken cancellationToken)
        {
            var pendingEvents = await _context.RawEvents
                .Where(e => e.EntityType == "deployment" && e.ProcessedAt == null)
                .OrderBy(e => e.OccurredAt)
                .ToListAsync(cancellationToken);

            int processedCount = 0;

            foreach (var rawEvent in pendingEvents)
            {
                try
                {
                    var gitLabDeployment = JsonSerializer.Deserialize<GitLabDeployment>(rawEvent.Payload);
                    if (gitLabDeployment == null)
                    {
                        rawEvent.ProcessedAt = DateTime.UtcNow;
                        rawEvent.Status = ProcessingStatus.Failed;
                        rawEvent.ErrorMessage = "Payload deserialization failed.";
                        continue;
                    }

                    // Resolve Repository
                    // EntityId format: "{ProjectId}-{DeploymentId}"
                    var parts = rawEvent.EntityId.Split('-');
                    if (parts.Length < 2)
                    {
                         rawEvent.ProcessedAt = DateTime.UtcNow;
                         rawEvent.Status = ProcessingStatus.Failed;
                         rawEvent.ErrorMessage = "Invalid EntityId format.";
                         continue;
                    }
                    var projectId = parts[0];

                    var repository = await _context.Repositories
                        .FirstOrDefaultAsync(r => r.ExternalId == projectId && r.IntegrationId == rawEvent.IntegrationId, cancellationToken);

                    if (repository == null)
                    {
                        rawEvent.ProcessedAt = DateTime.UtcNow;
                        rawEvent.Status = ProcessingStatus.Failed;
                        rawEvent.ErrorMessage = $"Repository not found for ProjectId: {projectId}";
                        continue;
                    }

                    // Resolve Service via ServiceRepository
                    // Assuming a repository belongs to a service. If multiple, we might pick one or duplicate?
                    // For now, let's pick the first one.
                    var serviceRepo = await _context.ServiceRepositories
                        .FirstOrDefaultAsync(sr => sr.RepositoryId == repository.Id, cancellationToken);
                    
                    if (serviceRepo == null)
                    {
                        // Some repositories might not be linked to a service yet.
                        // We can either skip or create a deployment without service? 
                        // But Deployment.ServiceId is required.
                        rawEvent.ProcessedAt = DateTime.UtcNow;
                        rawEvent.Status = ProcessingStatus.Failed;
                        rawEvent.ErrorMessage = "No Service linked to this Repository.";
                        continue;
                    }

                    // Check if Deployment exists
                    var deployment = await _context.Deployments
                        .FirstOrDefaultAsync(d => d.ServiceId == serviceRepo.ServiceId && d.ExternalId == gitLabDeployment.Id.ToString(), cancellationToken);

                    if (deployment == null)
                    {
                        deployment = new Deployment
                        {
                            ServiceId = serviceRepo.ServiceId,
                            ExternalId = gitLabDeployment.Id.ToString(),
                            Environment = gitLabDeployment.Environment?.Name ?? "unknown",
                            Status = gitLabDeployment.Status,
                            StartedAt = gitLabDeployment.CreatedAt.ToUniversalTime(),
                            DeployedAt = gitLabDeployment.CreatedAt.ToUniversalTime(), // Approximation if FinishedAt not available
                        };
                        
                        // Try to link Commit
                        if (!string.IsNullOrEmpty(gitLabDeployment.Sha))
                        {
                             var commit = await _context.Commits
                                .FirstOrDefaultAsync(c => c.Sha == gitLabDeployment.Sha, cancellationToken);
                             if (commit != null)
                             {
                                 deployment.CommitId = commit.Id;
                             }
                        }

                        // Try to link TriggerActor (User)
                        if (gitLabDeployment.User != null)
                        {
                            var toolAccount = await _context.ToolAccounts
                                .FirstOrDefaultAsync(ta => ta.IntegrationId == rawEvent.IntegrationId && ta.ExternalId == gitLabDeployment.User.Id.ToString(), cancellationToken);
                            
                            if (toolAccount != null)
                            {
                                deployment.TriggerActorId = toolAccount.UserId;
                            }
                        }

                        _context.Deployments.Add(deployment);
                    }
                    else
                    {
                        deployment.Status = gitLabDeployment.Status;
                        deployment.DeployedAt = gitLabDeployment.CreatedAt.ToUniversalTime(); // Update
                        
                        // Update TriggerActor if strictly needed (usually immutable but safe to check)
                        if (deployment.TriggerActorId == null && gitLabDeployment.User != null)
                        {
                            var toolAccount = await _context.ToolAccounts
                                .FirstOrDefaultAsync(ta => ta.IntegrationId == rawEvent.IntegrationId && ta.ExternalId == gitLabDeployment.User.Id.ToString(), cancellationToken);
                             
                            if (toolAccount != null)
                            {
                                deployment.TriggerActorId = toolAccount.UserId;
                            }
                        }
                    }

                    rawEvent.ProcessedAt = DateTime.UtcNow;
                    rawEvent.Status = ProcessingStatus.Processed;
                    processedCount++;
                }
                catch (Exception ex)
                {
                    rawEvent.ProcessedAt = DateTime.UtcNow;
                    rawEvent.Status = ProcessingStatus.Failed;
                    rawEvent.ErrorMessage = ex.Message;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return processedCount;
        }
    }
}
