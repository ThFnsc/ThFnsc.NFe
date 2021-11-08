using RazorEngine;
using RazorEngine.Templating;
using System.Security.Cryptography;
using ThFnsc.NFe.Core.Services;

namespace ThFnsc.NFe.Services.RazorEngineRenderer;

public class RazorRenderer : IRazorRenderer
{
    public Task<string> RenderAsync(string templateKey, string template, object model)
    {
        if (string.IsNullOrWhiteSpace(templateKey))
        {
            using var sha = SHA512.Create();
            var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(template));
            templateKey = Convert.ToBase64String(hash);
        }
        var result = Engine.Razor.RunCompile(
            templateSource: template,
            name: templateKey,
            modelType: null,
            model: model);
        return Task.FromResult(result);
    }
}
