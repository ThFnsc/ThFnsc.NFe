using Microsoft.Extensions.DependencyInjection;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Infra.Applications;
using ThFnsc.NFe.Infra.IPMNF;

namespace ThFnsc.NFe.Configuration
{
    public static class DependencyInjectionConfigurations
    {
        public static IServiceCollection AddDependencyInjectionConfigs(this IServiceCollection services)
        {
            services.AddSingleton<ITownHallApiClient, IPMNFApiClient>();
            services.AddScoped<NFeAppService>();
            services.AddScoped<SMTPAppService>();
            return services;
        }
    }
}
