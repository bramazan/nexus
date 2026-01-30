using Microsoft.EntityFrameworkCore;
using Nexus.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Nexus.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Workspace> Workspaces { get; }
        DbSet<User> Users { get; }
        DbSet<Team> Teams { get; }
        DbSet<TeamMember> TeamMembers { get; }
        DbSet<Integration> Integrations { get; }
        DbSet<ToolAccount> ToolAccounts { get; }
        DbSet<RawEvent> RawEvents { get; }
        
        DbSet<Service> Services { get; }
        DbSet<Repository> Repositories { get; }
        DbSet<ServiceRepository> ServiceRepositories { get; }
        
        DbSet<Branch> Branches { get; }
        DbSet<Commit> Commits { get; }
        DbSet<CodeChange> CodeChanges { get; }
        DbSet<PullRequest> PullRequests { get; }
        DbSet<PullRequestReview> PullRequestReviews { get; }
        DbSet<AiSignal> AiSignals { get; }
        
        DbSet<Sprint> Sprints { get; }
        DbSet<Issue> Issues { get; }
        DbSet<IssueEvent> IssueEvents { get; }
        DbSet<IssueEstimate> IssueEstimates { get; }
        DbSet<IssueAssignee> IssueAssignees { get; }
        DbSet<IssueSprintLink> IssueSprintLinks { get; }
        DbSet<BranchIssueLink> BranchIssueLinks { get; }
        
        DbSet<Deployment> Deployments { get; }
        DbSet<Release> Releases { get; }
        DbSet<Incident> Incidents { get; }
        DbSet<UserAvailability> UserAvailabilities { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
