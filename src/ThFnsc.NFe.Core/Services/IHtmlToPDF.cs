using System.Threading.Tasks;

namespace ThFnsc.NFe.Core.Services
{
    public interface IHtmlToPDF
    {
        Task<byte[]> ConvertHTMLToPDF(string html);
    }
}