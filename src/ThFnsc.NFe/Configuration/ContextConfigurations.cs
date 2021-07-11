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
            services.AddDbContext<NFContext>(opt =>
                opt.UseMySQL(configuration.GetConnectionString("Default")));

            using var sp = services.BuildServiceProvider();
            sp.GetRequiredService<ILogger<NFContext>>().LogInformation("Ensuring schema is created...");
            sp.GetRequiredService<NFContext>().Database.EnsureCreated();

            return services;
        }
    }
}
