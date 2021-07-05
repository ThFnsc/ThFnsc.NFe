using RazorLight;
using System.Threading.Tasks;

namespace ThFnsc.NFe.Infra.Services
{
    public static class RazorRenderer
    {
        public static async Task<string> RenderAsync<T>(string templateKey, string template, T model)
        {
            var engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(RazorRenderer))
                .SetOperatingAssembly(typeof(RazorRenderer).Assembly)
                .UseMemoryCachingProvider()
                .Build();

            string result = await engine.CompileRenderStringAsync(templateKey, template, model);
            return result;
        }
    }
}
