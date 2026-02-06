using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Application.Common.Models;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Application.GitLab.Commands
{
    public record ProcessMembersCommand : IRequest<int>;

    public class ProcessMembersCommandHandler : IRequestHandler<ProcessMembersCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public ProcessMembersCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(ProcessMembersCommand request, CancellationToken cancellationToken)
        {
            var pendingEvents = await _context.RawEvents
                .Where(e => (e.EntityType == "members" || e.EntityType == "member") && e.ProcessedAt == null)
                .OrderBy(e => e.OccurredAt)
                .Take(1000)
                .ToListAsync(cancellationToken);

            int processedCount = 0;

            foreach (var rawEvent in pendingEvents)
            {
                try
                {
                    // GitLab member API returns a list of members usually, but raw_events might store them individually if synced that way?
                    // SyncGitLabRawDataCommand: "members" => api/v4/projects/.../members
                    // This returns an array array of members.
                    // The raw_events table stores the payload. If SyncAllRepositoriesGenericCommand stores the WHOLE array as one event?
                    // Let's check SyncGitLabRawDataCommand...
                    // It uses _gitLabConnector.GetRawDataAsync which usually returns the JSON response.
                    // If it returns a List, we need to handle that.
                    // Assuming Payload is a List<GitLabMember>.

                    List<GitLabMember> members;
                    try 
                    {
                        members = JsonSerializer.Deserialize<List<GitLabMember>>(rawEvent.Payload) ?? new List<GitLabMember>();
                    }
                    catch (JsonException)
                    {
                        // Maybe it is a single object?
                        var singleInfo = JsonSerializer.Deserialize<GitLabMember>(rawEvent.Payload);
                        members = singleInfo != null ? new List<GitLabMember> { singleInfo } : new List<GitLabMember>();
                    }

                    if (!members.Any())
                    {
                        rawEvent.ProcessedAt = DateTime.UtcNow;
                        rawEvent.Status = ProcessingStatus.Processed; // Empty list is fine
                        continue;
                    }

                    foreach (var gitLabMember in members)
                    {
                        // Check if ToolAccount exists
                        // ExternalId = gitLabMember.Id.ToString()
                        var toolAccount = await _context.ToolAccounts
                            .FirstOrDefaultAsync(ta => ta.IntegrationId == rawEvent.IntegrationId && ta.ExternalId == gitLabMember.Id.ToString(), cancellationToken);

                        if (toolAccount == null)
                        {
                            // Create User first
                            // We don't have email in GitLabMember (usually). 
                            // Construct dummy email or search by name?
                            // Strategy: Search by Name. If found, link. If not, create new User.
                            
                            var user = await _context.Users
                                .FirstOrDefaultAsync(u => u.FullName == gitLabMember.Name, cancellationToken);

                            if (user == null)
                            {
                                // Construct a placeholder email
                                var dummyEmail = $"{gitLabMember.Username}@gitlab.nexus.placeholder";
                                
                                user = new User
                                {
                                    FullName = gitLabMember.Name,
                                    Email = dummyEmail
                                };
                                _context.Users.Add(user);
                                await _context.SaveChangesAsync(cancellationToken); // Need ID
                            }

                            toolAccount = new ToolAccount
                            {
                                UserId = user.Id,
                                IntegrationId = rawEvent.IntegrationId,
                                ExternalId = gitLabMember.Id.ToString(),
                                Username = gitLabMember.Username,
                                DisplayName = gitLabMember.Name,
                                IsActive = gitLabMember.State == "active",
                                ExternalMetadata = JsonSerializer.Serialize(new { AccessLevel = gitLabMember.AccessLevel })
                            };
                            _context.ToolAccounts.Add(toolAccount);
                        }
                        else
                        {
                            // Update
                            toolAccount.Username = gitLabMember.Username;
                            toolAccount.DisplayName = gitLabMember.Name;
                            toolAccount.IsActive = gitLabMember.State == "active";
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
