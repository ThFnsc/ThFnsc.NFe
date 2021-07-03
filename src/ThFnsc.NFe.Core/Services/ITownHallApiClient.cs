using System.Threading.Tasks;
using ThFnsc.NFe.Core.Models;
using ThFnsc.NFe.Data.Entities;

namespace ThFnsc.NFe.Core.Services
{
    public interface ITownHallApiClient
    {
        Task<string> GenerateXMLAsync(IssuedNFe nfe);

        Task<TownHallResponse> GenerateFromXMLAsync(IssuedNFe nfe, string xml);
    }
}
