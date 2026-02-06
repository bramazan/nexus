using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncMergeRequestDiscussionsCommand(Guid IntegrationId, int ProjectId, int MergeRequestId) : IRequest<int>;

    public class SyncMergeRequestDiscussionsCommandHandler : IRequestHandler<SyncMergeRequestDiscussionsCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGitLabConnector _connector;

        public SyncMergeRequestDiscussionsCommandHandler(IApplicationDbContext context, IGitLabConnector connector)
        {
            _context = context;
            _connector = connector;
        }

        public async Task<int> Handle(SyncMergeRequestDiscussionsCommand request, CancellationToken cancellationToken)
        {
            var notesJson = await _connector.GetMergeRequestNotesAsync(request.IntegrationId, request.ProjectId, request.MergeRequestId);
            
            // Raw Event Creation
            var rawEvent = new RawEvent
            {
                Id = Guid.NewGuid(),
                IntegrationId = request.IntegrationId,
                Source = "GitLab",
                EntityType = "review", // Discussion/Note
                EntityId = $"{request.ProjectId}-{request.MergeRequestId}-notes",
                Payload = notesJson.GetRawText(),
                OccurredAt = DateTime.UtcNow, // Notes are a collection, so use current time or try to find latest note time? keeping simple.
                IngestedAt = DateTime.UtcNow,
                Status = ProcessingStatus.Pending
            };

            // Check if exists to update
            var existingEvent = await _context.RawEvents
                .FirstOrDefaultAsync(r => 
                    r.IntegrationId == request.IntegrationId && 
                    r.EntityType == "review" && 
                    r.EntityId == rawEvent.EntityId, 
                    cancellationToken);

            if (existingEvent != null)
            {
                existingEvent.Payload = rawEvent.Payload;
                existingEvent.IngestedAt = DateTime.UtcNow;
                existingEvent.Status = ProcessingStatus.Pending; // Reprocess
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
