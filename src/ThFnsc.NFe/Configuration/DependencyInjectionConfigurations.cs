using Microsoft.Extensions.DependencyInjection;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Infra.Applications;
using ThFnsc.NFe.Infra.Services;
using ThFnsc.NFe.Infra.Services.Chrome;
using ThFnsc.NFe.Services.ContaJa.Notifier;

namespace ThFnsc.NFe.Configuration
{
    public static class DependencyInjectionConfigurations
    {
        public static IServiceCollection AddDependencyInjectionConfigs(this IServiceCollection services)
        {
            services.AddScoped<NFeAppService>();
            services.AddScoped<SMTPAppService>();
            services.AddScoped<ScheduledGenerationAppService>();

            services.AddSingleton<IHtmlToPDF, PuppeteerSharpHtmlToPDF>();
            services.AddSingleton<IRazorRenderer, RazorRenderer>();
            services.AddSingleton<IChromePathFinder, DumbChromeFinder>();
            services.AddSingleton<INFNotifier, ContaJaNFeNotifier>();
            return services;
        }
    }
}
