using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncMergeRequestApprovalsCommand(Guid IntegrationId, int ProjectId, int MergeRequestId) : IRequest<int>;

    public class SyncMergeRequestApprovalsCommandHandler : IRequestHandler<SyncMergeRequestApprovalsCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGitLabConnector _connector;

        public SyncMergeRequestApprovalsCommandHandler(IApplicationDbContext context, IGitLabConnector connector)
        {
            _context = context;
            _connector = connector;
        }

        public async Task<int> Handle(SyncMergeRequestApprovalsCommand request, CancellationToken cancellationToken)
        {
            var approvalsJson = await _connector.GetMergeRequestApprovalsAsync(request.IntegrationId, request.ProjectId, request.MergeRequestId);
            
            // Raw Event Creation
            var rawEvent = new RawEvent
            {
                Id = Guid.NewGuid(),
                IntegrationId = request.IntegrationId,
                Source = "GitLab",
                EntityType = "review_approval", // Specific type for Approvals
                EntityId = $"{request.ProjectId}-{request.MergeRequestId}-approvals",
                Payload = approvalsJson.GetRawText(),
                OccurredAt = DateTime.UtcNow,
                IngestedAt = DateTime.UtcNow,
                Status = ProcessingStatus.Pending
            };

            // Check if exists to update
            var existingEvent = await _context.RawEvents
                .FirstOrDefaultAsync(r => 
                    r.IntegrationId == request.IntegrationId && 
                    r.EntityType == "review_approval" && 
                    r.EntityId == rawEvent.EntityId, 
                    cancellationToken);

            if (existingEvent != null)
            {
                existingEvent.Payload = rawEvent.Payload;
                existingEvent.IngestedAt = DateTime.UtcNow;
                existingEvent.Status = ProcessingStatus.Pending; // Reprocess to capture new approvals
                existingEvent.ProcessedAt = null;
                existingEvent.ErrorMessage = null;
            }
            else
            {
                _context.RawEvents.Add(rawEvent);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return 1;
        }
    }
}
