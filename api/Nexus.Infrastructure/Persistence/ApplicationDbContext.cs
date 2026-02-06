using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;
using Nexus.Infrastructure.Persistence.Extensions;
using System.Reflection;

namespace Nexus.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Workspace> Workspaces => Set<Workspace>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
        public DbSet<Integration> Integrations => Set<Integration>();
        public DbSet<ToolAccount> ToolAccounts => Set<ToolAccount>();
        public DbSet<RawEvent> RawEvents => Set<RawEvent>();
        
        public DbSet<Service> Services => Set<Service>();
        public DbSet<Repository> Repositories => Set<Repository>();
        public DbSet<ServiceRepository> ServiceRepositories => Set<ServiceRepository>();
        
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Commit> Commits => Set<Commit>();
        public DbSet<CodeChange> CodeChanges => Set<CodeChange>();
        public DbSet<PullRequest> PullRequests => Set<PullRequest>();
        public DbSet<PullRequestReview> PullRequestReviews => Set<PullRequestReview>();
        public DbSet<Pipeline> Pipelines => Set<Pipeline>();
        public DbSet<AiSignal> AiSignals => Set<AiSignal>();
        
        public DbSet<Sprint> Sprints => Set<Sprint>();
        public DbSet<Issue> Issues => Set<Issue>();
        public DbSet<IssueEvent> IssueEvents => Set<IssueEvent>();
        public DbSet<IssueEstimate> IssueEstimates => Set<IssueEstimate>();
        public DbSet<IssueAssignee> IssueAssignees => Set<IssueAssignee>();
        public DbSet<IssueSprintLink> IssueSprintLinks => Set<IssueSprintLink>();
        public DbSet<BranchIssueLink> BranchIssueLinks => Set<BranchIssueLink>();
        
        public DbSet<Deployment> Deployments => Set<Deployment>();
        public DbSet<Release> Releases => Set<Release>();
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<ServiceMetric> ServiceMetrics { get; set; }
        public DbSet<UserAvailability> UserAvailabilities => Set<UserAvailability>();
        public DbSet<MetricThreshold> MetricThresholds => Set<MetricThreshold>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.ApplySnakeCaseNamingConvention();

            base.OnModelCreating(builder);
        }
    }
}
