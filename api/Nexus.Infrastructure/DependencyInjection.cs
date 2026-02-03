using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Application.Common.Interfaces;
using Nexus.Infrastructure.Persistence;

namespace Nexus.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddHttpClient(); // Required for Connectors

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ApplicationDbContextInitialiser>();
            services.AddScoped<IGitLabConnector, Connectors.GitLabConnector>();
            services.AddScoped<IJiraConnector, Connectors.JiraConnector>();
            services.AddScoped<IInstanaConnector, Connectors.InstanaConnector>();
            services.AddScoped<INormalizationService, Services.NormalizationService>();

            return services;
        }
    }
}
