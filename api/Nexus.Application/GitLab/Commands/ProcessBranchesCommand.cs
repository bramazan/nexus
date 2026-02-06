using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Application.Common.Models;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Application.GitLab.Commands
{
    public record ProcessBranchesCommand : IRequest<int>;

    public class ProcessBranchesCommandHandler : IRequestHandler<ProcessBranchesCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public ProcessBranchesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(ProcessBranchesCommand request, CancellationToken cancellationToken)
        {
            var pendingEvents = await _context.RawEvents
                .Where(e => (e.EntityType == "branches" || e.EntityType == "branch") && e.ProcessedAt == null)
                .OrderBy(e => e.OccurredAt)
                .ToListAsync(cancellationToken);

            int processedCount = 0;

            foreach (var rawEvent in pendingEvents)
            {
                try
                {
                    List<GitLabBranch> branches;
                    try
                    {
                        branches = JsonSerializer.Deserialize<List<GitLabBranch>>(rawEvent.Payload) ?? new List<GitLabBranch>();
                    }
                    catch (JsonException)
                    {
                        var single = JsonSerializer.Deserialize<GitLabBranch>(rawEvent.Payload);
                        branches = single != null ? new List<GitLabBranch> { single } : new List<GitLabBranch>();
                    }

                    if (!branches.Any())
                    {
                        rawEvent.ProcessedAt = DateTime.UtcNow;
                        rawEvent.Status = ProcessingStatus.Processed;
                        continue;
                    }

                    // Resolve Repository
                    // EntityId might be random GUID if Sync was generic.
                    // Fallback strategy: Use Commit SHA from Branch to find Commit -> Repository.
                    
                    var repository = await _context.Repositories
                        .FirstOrDefaultAsync(r => r.IntegrationId == rawEvent.IntegrationId && r.ExternalId == rawEvent.EntityId, cancellationToken);

                    if (repository == null && branches.Any())
                    {
                        var firstCommitSha = branches.FirstOrDefault(b => b.Commit != null)?.Commit?.Id;
                        if (!string.IsNullOrEmpty(firstCommitSha))
                        {
                            var commit = await _context.Commits
                                .Include(c => c.Repository)
                                .FirstOrDefaultAsync(c => c.Sha == firstCommitSha && c.Repository.IntegrationId == rawEvent.IntegrationId, cancellationToken);
                            
                            if (commit != null)
                            {
                                repository = commit.Repository;
                            }
                        }
                    }

                    if (repository != null)
                    {
                        foreach (var glBranch in branches)
                        {
                            var branch = await _context.Branches
                                .FirstOrDefaultAsync(b => b.RepositoryId == repository.Id && b.Name == glBranch.Name, cancellationToken);
                            
                            if (branch == null)
                            {
                                branch = new Branch
                                {
                                    RepositoryId = repository.Id,
                                    Name = glBranch.Name,
                                    UpdatedAt = DateTime.UtcNow // No Date in GitLabBranch DTO? Check schema.
                                    // GitlabBranch usually has "commit" object which has committed_date.
                                };
                                
                                if (glBranch.Commit != null)
                                {
                                    branch.UpdatedAt = glBranch.Commit.CreatedAt.ToUniversalTime();
                                }

                                _context.Branches.Add(branch);
                            }
                            else
                            {
                                // Update
                                if (glBranch.Commit != null)
                                {
                                    branch.UpdatedAt = glBranch.Commit.CreatedAt.ToUniversalTime();
                                }
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
