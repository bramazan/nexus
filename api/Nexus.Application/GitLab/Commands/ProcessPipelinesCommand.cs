using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Application.Common.Models;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Application.GitLab.Commands
{
    public record ProcessPipelinesCommand : IRequest<int>;

    public class ProcessPipelinesCommandHandler : IRequestHandler<ProcessPipelinesCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public ProcessPipelinesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(ProcessPipelinesCommand request, CancellationToken cancellationToken)
        {
            // 1. Fetch pending raw events for pipelines
            var pendingEvents = await _context.RawEvents
                .Where(e => e.EntityType == "pipeline" && e.ProcessedAt == null)
                .OrderBy(e => e.OccurredAt)
                .ToListAsync(cancellationToken);

            int processedCount = 0;

            foreach (var rawEvent in pendingEvents)
            {
                try
                {
                    // 2. Deserialize payload
                    var gitLabPipeline = JsonSerializer.Deserialize<GitLabPipeline>(rawEvent.Payload);

                    if (gitLabPipeline == null)
                    {
                        rawEvent.ProcessedAt = DateTime.UtcNow;
                        rawEvent.Status = ProcessingStatus.Failed;
                        rawEvent.ErrorMessage = "Payload deserialization failed or is null.";
                        continue;
                    }

                    // 3. Find or Create Domain Entity
                    // We need to resolve RepositoryId from the project ID in the raw event or payload.
                    // RawEvent has EntityId format "{ProjectId}-{PipelineId}"
                    // But we can also look up Repository by ExternalId using gitLabPipeline.ProjectId (if available in DTO) or from parsing EntityId.
                    
                    var parts = rawEvent.EntityId.Split('-');
                    if (parts.Length < 2)
                    {
                        rawEvent.ProcessedAt = DateTime.UtcNow;
                        rawEvent.Status = ProcessingStatus.Failed;
                        rawEvent.ErrorMessage = "Invalid EntityId format.";
                        continue;
                    }

                    var projectId = parts[0]; 
                    var pipelineId = parts[1];

                    var repository = await _context.Repositories
                        .FirstOrDefaultAsync(r => r.ExternalId == projectId && r.IntegrationId == rawEvent.IntegrationId, cancellationToken);

                    if (repository == null)
                    {
                        rawEvent.ProcessedAt = DateTime.UtcNow;
                        rawEvent.Status = ProcessingStatus.Failed;
                        rawEvent.ErrorMessage = $"Repository not found for ProjectId: {projectId}";
                        continue;
                    }

                    var pipeline = await _context.Pipelines
                        .FirstOrDefaultAsync(p => p.IntegrationId == rawEvent.IntegrationId && p.ExternalId == gitLabPipeline.Id, cancellationToken);

                    if (pipeline == null)
                    {
                        pipeline = new Pipeline
                        {
                            IntegrationId = rawEvent.IntegrationId,
                            RepositoryId = repository.Id,
                            ExternalId = gitLabPipeline.Id,
                            Iid = gitLabPipeline.Iid,
                            Status = gitLabPipeline.Status,
                            Source = gitLabPipeline.Source,
                            Ref = gitLabPipeline.Ref,
                            Sha = gitLabPipeline.Sha,
                            WebUrl = gitLabPipeline.WebUrl,
                            CreatedAt = gitLabPipeline.CreatedAt.ToUniversalTime(),
                            UpdatedAt = gitLabPipeline.UpdatedAt.ToUniversalTime()
                        };
                        _context.Pipelines.Add(pipeline);
                    }
                    else
                    {
                        // Update existing
                        pipeline.Status = gitLabPipeline.Status;
                        pipeline.UpdatedAt = gitLabPipeline.UpdatedAt.ToUniversalTime();
                        pipeline.Sha = gitLabPipeline.Sha;
                        pipeline.WebUrl = gitLabPipeline.WebUrl;
                    }

                    // 4. Mark RawEvent as Processed
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
