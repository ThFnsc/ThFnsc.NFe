using RazorEngine;
using RazorEngine.Templating;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Services;

namespace ThFnsc.NFe.Infra.Services
{
    public class RazorRenderer : IRazorRenderer
    {
        public Task<string> RenderAsync(string templateKey, string template, object model)
        {
            var result = Engine.Razor.RunCompile(template, templateKey, null, model);
            return Task.FromResult(result);
        }
    }
}
