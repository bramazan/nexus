using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Application.Common.Models;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Application.GitLab.Commands
{
    public record ProcessJobsCommand : IRequest<int>;

    public class ProcessJobsCommandHandler : IRequestHandler<ProcessJobsCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public ProcessJobsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(ProcessJobsCommand request, CancellationToken cancellationToken)
        {
            var pendingEvents = await _context.RawEvents
                .Where(e => (e.EntityType == "jobs" || e.EntityType == "job") && e.ProcessedAt == null)
                .OrderBy(e => e.OccurredAt)
                .ToListAsync(cancellationToken);

            int processedCount = 0;

            foreach (var rawEvent in pendingEvents)
            {
                try
                {
                    using (JsonDocument doc = JsonDocument.Parse(rawEvent.Payload))
                    {
                        if (doc.RootElement.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var element in doc.RootElement.EnumerateArray())
                            {
                                await ProcessJobElement(_context, rawEvent, element, cancellationToken);
                            }
                        }
                        else if (doc.RootElement.ValueKind == JsonValueKind.Object)
                        {
                            await ProcessJobElement(_context, rawEvent, doc.RootElement, cancellationToken);
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

        private async Task ProcessJobElement(IApplicationDbContext _context, RawEvent rawEvent, JsonElement element, CancellationToken cancellationToken)
        {
            int jobId = element.GetProperty("id").GetInt32();
            
            // Try get pipeline info
            int? pipelineId = null;
            if (element.TryGetProperty("pipeline", out var pipelineProp))
            {
                if (pipelineProp.TryGetProperty("id", out var pId))
                    pipelineId = pId.GetInt32();
            }
            
            string? commitSha = null;
            string? jobRef = null;
            if (element.TryGetProperty("commit", out var commitProp))
            {
                if (commitProp.TryGetProperty("id", out var shaProp))
                    commitSha = shaProp.GetString();
            }
            if (element.TryGetProperty("ref", out var refProp))
                jobRef = refProp.GetString();
            
            // Find Pipeline in DB
            Guid? dbPipelineId = null;
            if (pipelineId.HasValue)
            {
                var pipeline = await _context.Pipelines
                    .FirstOrDefaultAsync(p => p.IntegrationId == rawEvent.IntegrationId && p.ExternalId == pipelineId.Value, cancellationToken);
                if (pipeline != null) dbPipelineId = pipeline.Id;
            }
            
            // Fallback: Try match by SHA
            if (!dbPipelineId.HasValue && !string.IsNullOrEmpty(commitSha))
            {
                    var pipeline = await _context.Pipelines
                    .Where(p => p.IntegrationId == rawEvent.IntegrationId && p.Sha == commitSha)
                    .OrderByDescending(p => p.CreatedAt)
                    .FirstOrDefaultAsync(cancellationToken);
                    
                    if (pipeline != null) dbPipelineId = pipeline.Id;
            }

            if (dbPipelineId.HasValue)
            {
                // Process Job
                var job = await _context.Jobs.FirstOrDefaultAsync(j => j.PipelineId == dbPipelineId.Value && j.ExternalId == jobId.ToString(), cancellationToken);
                if (job == null)
                {
                    var gitLabJob = JsonSerializer.Deserialize<GitLabJob>(element.GetRawText());
                    if (gitLabJob != null)
                    {
                        job = new Job
                        {
                            PipelineId = dbPipelineId.Value,
                            ExternalId = jobId.ToString(),
                            Name = gitLabJob.Name,
                            Stage = gitLabJob.Stage,
                            Status = gitLabJob.Status,
                            CreatedAt = gitLabJob.CreatedAt.ToUniversalTime(),
                            StartedAt = gitLabJob.StartedAt?.ToUniversalTime(),
                            FinishedAt = gitLabJob.FinishedAt?.ToUniversalTime(),
                            Duration = gitLabJob.Duration
                        };
                        _context.Jobs.Add(job);
                    }
                }
            }
        }
    }
}
