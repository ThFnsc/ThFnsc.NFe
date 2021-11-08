namespace ThFnsc.NFe.Core.Services
{
    public interface IRazorRenderer
    {
        Task<string> RenderAsync(string templateKey, string template, object model);
    }
}