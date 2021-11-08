using Hangfire;
using Hangfire.MySql;

namespace ThFnsc.NFe.Configuration;

public static class HangfireConfigurations
{
    public static IServiceCollection AddHangfireConfigs(this IServiceCollection services, IConfiguration conf)
    {
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseStorage(new MySqlStorage(conf.GetConnectionString("Default") + ";Allow User Variables=true", new MySqlStorageOptions
            {
                TablesPrefix = "HF_",
                PrepareSchemaIfNecessary = true
            })));

        services.AddHangfireServer();

        return services;
    }
}
