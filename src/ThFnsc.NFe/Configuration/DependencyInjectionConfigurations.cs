using Microsoft.Extensions.DependencyInjection;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Infra.Applications;
using ThFnsc.NFe.Infra.IPMNF;
using ThFnsc.NFe.Infra.Services;

namespace ThFnsc.NFe.Configuration
{
    public static class DependencyInjectionConfigurations
    {
        public static IServiceCollection AddDependencyInjectionConfigs(this IServiceCollection services)
        {
            services.AddScoped<NFeAppService>();
            services.AddScoped<SMTPAppService>();
            services.AddScoped<ScheduledGenerationAppService>();

            services.AddSingleton<IHtmlToPDF, HtmlToPDF>();
            services.AddSingleton<IRazorRenderer, RazorRenderer>();
            return services;
        }
    }
}
