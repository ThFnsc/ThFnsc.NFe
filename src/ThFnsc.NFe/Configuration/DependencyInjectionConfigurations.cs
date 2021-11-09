using Microsoft.Extensions.DependencyInjection;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Infra.Applications;
using ThFnsc.NFe.Services.ContaJa.Notifier;
using ThFnsc.NFe.Services.PuppeteerHTMLToPDF;
using ThFnsc.NFe.Services.RazorEngineRenderer;
using ThFnsc.NFe.Services.SMTP;

namespace ThFnsc.NFe.Configuration
{
    public static class DependencyInjectionConfigurations
    {
        public static IServiceCollection AddDependencyInjectionConfigs(this IServiceCollection services)
        {
            services.AddScoped<NFeAppService>();
            services.AddScoped<ScheduledGenerationAppService>();
            services.AddScoped<NotificationAppService>();

            services.AddSingleton<IHtmlToPDF, PuppeteerSharpHtmlToPDF>();
            services.AddSingleton<IRazorRenderer, RazorRenderer>();

            services.AddScoped<INFNotifier, ContaJaNFeNotifier>();
            services.AddScoped<INFNotifier, SMTPNotifier>();

            return services;
        }
    }
}
