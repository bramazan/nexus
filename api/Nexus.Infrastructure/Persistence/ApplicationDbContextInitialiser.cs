using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nexus.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nexus.Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsNpgsql())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Default Workspace
            if (!await _context.Workspaces.AnyAsync())
            {
                var workspace = new Workspace
                {
                    Name = "Nexus Engineering"
                };
                
                _context.Workspaces.Add(workspace);
                await _context.SaveChangesAsync();

                // Default User
                var user = new User
                {
                    FullName = "System Admin",
                    Email = "admin@nexus.com"
                };
                
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Integrations
                // Jira Integration
                _context.Integrations.Add(new Integration
                {
                    WorkspaceId = workspace.Id,
                    Type = "jira",
                    Name = "Main Jira Instance",
                    Config = "{ \"baseUrl\": \"https://your-domain.atlassian.net\", \"email\": \"admin@nexus.com\", \"apiToken\": \"YOUR_API_TOKEN\" }"
                });

                // GitLab Integration
                _context.Integrations.Add(new Integration
                {
                    WorkspaceId = workspace.Id,
                    Type = "gitlab",
                    Name = "Corporate GitLab",
                    Config = "{ \"baseUrl\": \"https://gitlab.com\", \"accessToken\": \"YOUR_ACCESS_TOKEN\", \"groupId\": \"12345\" }"
                });

                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Default data seeded: Workspace, User, and Integrations (Jira, GitLab).");
            }
        }
    }
}
