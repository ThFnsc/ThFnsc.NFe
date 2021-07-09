using System.Threading.Tasks;

namespace ThFnsc.NFe.Core.Services
{
    public interface IChromePathFinder
    {
        Task<string> FindChromeAsync();
    }
}
