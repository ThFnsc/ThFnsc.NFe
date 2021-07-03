using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThFnsc.NFe.Data.Context;

namespace ThFnsc.NFe.Configuration
{
    public static class ContextConfigurations
    {
        public static IServiceCollection AddContextConfigs(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.AddDbContext<NFContext>(opt =>
                opt.UseMySQL(configuration.GetConnectionString("Default")));
            return services;
        }
    }
}
