using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ThFnsc.NFe.Data.Context;

namespace ThFnsc.NFe.Configuration
{
    public static class ContextConfigurations
    {
        public static IServiceCollection AddContextConfigs(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            var dbConnectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<NFContext>(opt =>
                opt.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)), contextLifetime: ServiceLifetime.Transient);

            using var sp = services.BuildServiceProvider();

            sp.GetRequiredService<ILogger<NFContext>>().LogInformation("Migrating database...");
            sp.GetRequiredService<NFContext>().Database.Migrate();

            return services;
        }
    }
}
