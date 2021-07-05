using RazorEngine;
using RazorEngine.Templating;
using System.Threading.Tasks;

namespace ThFnsc.NFe.Infra.Services
{
    public static class RazorRenderer
    {
        public static async Task<string> RenderAsync(string templateKey, string template, object model)
        {
            var result = Engine.Razor.RunCompile(template, templateKey, null, model);
            return result;
        }
    }
}
