using Microsoft.Extensions.DependencyInjection;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Services.IPMNF;

namespace ThFnsc.NFe.Configuration
{
    public static class HttpClientsConfigurations
    {
        public static IServiceCollection AddHttpClientsConfigs(this IServiceCollection services)
        {
            services.AddHttpClient<ITownHallApiClient, IPMNFApiClient>();
            return services;
        }
    }
}
