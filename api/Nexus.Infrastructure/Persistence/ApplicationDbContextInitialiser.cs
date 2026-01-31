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
            // Seed logic has been moved to nexus/docs/tasks/seed.sql
            // This ensures data management is handled separately from code deployment.
            
            // Only keeping critical system user if absolutely necessary, but relying on SQL script is preferred now.
            if (!await _context.Users.AnyAsync())
            {
                _logger.LogInformation("Database is empty. Please run 'nexus/docs/tasks/seed.sql' to populate initial data.");
            }
            
            await Task.CompletedTask;
        }
    }
}
