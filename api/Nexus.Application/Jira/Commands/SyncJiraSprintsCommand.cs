using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;

namespace Nexus.Application.Jira.Commands
{
    public record SyncJiraSprintsCommand(Guid IntegrationId) : IRequest<int>;

    public class SyncJiraSprintsCommandHandler : IRequestHandler<SyncJiraSprintsCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IJiraConnector _jiraConnector;

        public SyncJiraSprintsCommandHandler(IApplicationDbContext context, IJiraConnector jiraConnector)
        {
            _context = context;
            _jiraConnector = jiraConnector;
        }

        public async Task<int> Handle(SyncJiraSprintsCommand request, CancellationToken cancellationToken)
        {
            var boards = await _jiraConnector.GetBoardsAsync(request.IntegrationId);
            int count = 0;

            foreach (var board in boards)
            {
                // Only sync sprints from Scrum boards
                if (board.Type == "scrum")
                {
                    try 
                    {
                        var sprints = await _jiraConnector.GetSprintsAsync(request.IntegrationId, board.Id);
                        
                        foreach (var jiraSprint in sprints)
                        {
                            var existingSprint = await _context.Sprints
                                .FirstOrDefaultAsync(s => s.IntegrationId == request.IntegrationId && s.Name == jiraSprint.Name, cancellationToken);
                            
                            if (existingSprint == null)
                            {
                                existingSprint = new Sprint
                                {
                                    IntegrationId = request.IntegrationId,
                                    Name = jiraSprint.Name,
                                    Status = jiraSprint.State,
                                    StartDate = jiraSprint.StartDate.HasValue ? DateOnly.FromDateTime(jiraSprint.StartDate.Value) : null,
                                    EndDate = jiraSprint.EndDate.HasValue ? DateOnly.FromDateTime(jiraSprint.EndDate.Value) : null,
                                };
                                _context.Sprints.Add(existingSprint);
                            }
                            else
                            {
                                existingSprint.Status = jiraSprint.State;
                                existingSprint.StartDate = jiraSprint.StartDate.HasValue ? DateOnly.FromDateTime(jiraSprint.StartDate.Value) : existingSprint.StartDate;
                                existingSprint.EndDate = jiraSprint.EndDate.HasValue ? DateOnly.FromDateTime(jiraSprint.EndDate.Value) : existingSprint.EndDate;
                            }
                            count++;
                        }
                    }
                    catch 
                    {
                        // Some boards might not support sprints or have permissions issues, continue
                    }
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return count;
        }
    }
}
