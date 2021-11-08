using Microsoft.EntityFrameworkCore;
using ThFnsc.NFe.Data.Context;

namespace ThFnsc.NFe.Configuration
{
    public static class ContextConfigurations
    {
        public static IServiceCollection AddContextConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NFContext>(opt =>
            {
                var cs = configuration.GetConnectionString("Default");
                opt.UseMySQL(cs);
            });

            using var sp = services.BuildServiceProvider();

            sp.GetRequiredService<ILogger<NFContext>>().LogInformation("Migrating database...");
            sp.GetRequiredService<NFContext>().Database.Migrate();

            return services;
        }
    }
}
