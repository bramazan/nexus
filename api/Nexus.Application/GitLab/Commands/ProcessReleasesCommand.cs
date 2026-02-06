using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Application.Common.Models;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Application.GitLab.Commands
{
    public record ProcessReleasesCommand : IRequest<int>;

    public class ProcessReleasesCommandHandler : IRequestHandler<ProcessReleasesCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public ProcessReleasesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(ProcessReleasesCommand request, CancellationToken cancellationToken)
        {
            var pendingEvents = await _context.RawEvents
                .Where(e => e.EntityType == "release" && e.ProcessedAt == null)
                .OrderBy(e => e.OccurredAt)
                .Take(1000)
                .ToListAsync(cancellationToken);

            int processedCount = 0;

            foreach (var rawEvent in pendingEvents)
            {
                try
                {
                    var gitLabRelease = JsonSerializer.Deserialize<GitLabRelease>(rawEvent.Payload);
                    if (gitLabRelease == null)
                    {
                        rawEvent.ProcessedAt = DateTime.UtcNow;
                        rawEvent.Status = ProcessingStatus.Failed;
                        rawEvent.ErrorMessage = "Payload deserialization failed.";
                        continue;
                    }

                    // Resolve Repository
                    var parts = rawEvent.EntityId.Split('-');
                    if (parts.Length < 2)
                    {
                         // Assuming EntityId for release is "{ProjectId}-{TagName}" but TagName might contain dashes.
                         // Or maybe existing logic uses different ID format?
                         // SyncReleasesCommand uses: EntityId = $"{request.ProjectId}-{release.TagName}"
                         // So we split by first dash? No, standard split might fail if tag has dashes.
                         // Better rely on ProjectId from finding repo by integration ID and maybe Iterate?
                         // Actually, we can just parse the ProjectId from the rawEvent.EntityId if we know the format exactly.
                         // But easier: We can iterate all Repositories matching IntegrationId? No, expensive.
                         // Let's rely on string split limit 2.
                    }
                    
                    // Safe split for ProjectId (first part)
                    var splitIndex = rawEvent.EntityId.IndexOf('-');
                    if (splitIndex == -1)
                    {
                         rawEvent.ProcessedAt = DateTime.UtcNow;
                         rawEvent.Status = ProcessingStatus.Failed;
                         rawEvent.ErrorMessage = "Invalid EntityId format.";
                         continue;
                    }
                    var projectId = rawEvent.EntityId.Substring(0, splitIndex);

                    var repository = await _context.Repositories
                        .FirstOrDefaultAsync(r => r.ExternalId == projectId && r.IntegrationId == rawEvent.IntegrationId, cancellationToken);

                    if (repository == null)
                    {
                        rawEvent.ProcessedAt = DateTime.UtcNow;
                        rawEvent.Status = ProcessingStatus.Failed;
                        rawEvent.ErrorMessage = $"Repository not found for ProjectId: {projectId}";
                        continue;
                    }

                    // Resolve Service
                    var serviceRepo = await _context.ServiceRepositories
                        .FirstOrDefaultAsync(sr => sr.RepositoryId == repository.Id, cancellationToken);
                    
                    if (serviceRepo == null)
                    {
                        rawEvent.ProcessedAt = DateTime.UtcNow;
                        rawEvent.Status = ProcessingStatus.Failed;
                        rawEvent.ErrorMessage = "No Service linked to this Repository.";
                        continue;
                    }

                    // Check existence
                    var release = await _context.Releases
                        .FirstOrDefaultAsync(r => r.ServiceId == serviceRepo.ServiceId && r.TagName == gitLabRelease.TagName, cancellationToken);

                    if (release == null)
                    {
                        release = new Release
                        {
                            ServiceId = serviceRepo.ServiceId,
                            Version = gitLabRelease.TagName, // Using TagName as Version
                            TagName = gitLabRelease.TagName,
                            CommitSha = gitLabRelease.Commit?.Id ?? gitLabRelease.Commit?.ShortId, 
                            ReleasedAt = gitLabRelease.ReleasedAt.ToUniversalTime(),
                            Description = gitLabRelease.Description
                        };

                        // Link Author
                        if (gitLabRelease.Author != null)
                        {
                            var toolAccount = await _context.ToolAccounts
                                .FirstOrDefaultAsync(ta => ta.IntegrationId == rawEvent.IntegrationId && ta.ExternalId == gitLabRelease.Author.Id.ToString(), cancellationToken);
                            
                            if (toolAccount == null)
                            {
                                // Create User/ToolAccount on the fly
                                var author = gitLabRelease.Author;
                                var user = await _context.Users.FirstOrDefaultAsync(u => u.FullName == author.Name, cancellationToken);
                                if (user == null)
                                {
                                    user = new User 
                                    { 
                                        FullName = author.Name, 
                                        Email = $"{author.Username}@gitlab.nexus.placeholder" 
                                    };
                                    _context.Users.Add(user);
                                    await _context.SaveChangesAsync(cancellationToken);
                                }

                                toolAccount = new ToolAccount
                                {
                                    UserId = user.Id,
                                    IntegrationId = rawEvent.IntegrationId,
                                    ExternalId = author.Id.ToString(),
                                    Username = author.Username,
                                    DisplayName = author.Name,
                                    IsActive = author.State == "active"
                                };
                                _context.ToolAccounts.Add(toolAccount);
                                await _context.SaveChangesAsync(cancellationToken);
                            }

                            release.AuthorId = toolAccount.UserId;
                        }

                        _context.Releases.Add(release);
                    }
                    else
                    {
                        release.Description = gitLabRelease.Description;
                        release.ReleasedAt = gitLabRelease.ReleasedAt.ToUniversalTime();
                        release.CommitSha = gitLabRelease.Commit?.Id ?? gitLabRelease.Commit?.ShortId;
                        
                         // Update Author if strict check
                         if (release.AuthorId == null && gitLabRelease.Author != null)
                         {
                              // Same creation logic... simplified for update:
                              var ta = await _context.ToolAccounts.FirstOrDefaultAsync(t => t.IntegrationId == rawEvent.IntegrationId && t.ExternalId == gitLabRelease.Author.Id.ToString(), cancellationToken);
                              if (ta != null) release.AuthorId = ta.UserId;
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
