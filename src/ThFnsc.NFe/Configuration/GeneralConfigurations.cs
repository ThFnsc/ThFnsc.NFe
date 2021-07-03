using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ThFnsc.NFe.Configuration
{
    public static class GeneralConfigurations
    {
        public static IServiceCollection AddGeneralConfigs(this IServiceCollection services, Microsoft.AspNetCore.Hosting.IWebHostEnvironment environment)
        {
            var mvcBuilder = services.AddRazorPages();
            services.AddServerSideBlazor();
            if (environment.IsDevelopment())
                mvcBuilder.AddRazorRuntimeCompilation();
            return services;
        }
    }
}
