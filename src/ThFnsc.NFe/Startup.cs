using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ThFnsc.NFe.Configuration;
using ThFnsc.NFe.Data.Context;
using ThFnsc.NFe.Infra.Services.Hangfire;

namespace ThFnsc.NFe
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddDependencyInjectionConfigs()
                .AddHttpClientsConfigs()
                .AddContextConfigs(Configuration)
                .AddGeneralConfigs(Environment)
                .AddHangfireConfigs(Configuration);

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            NFContext context,
            ILogger<Startup> logger)
        {
            logger.LogInformation("Migrating database...");
            context.Database.Migrate();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapHangfireDashboard("/HF", new DashboardOptions { Authorization = new[] { new AllowAllAuthorizationFilter() } });
            });
        }
    }
}
