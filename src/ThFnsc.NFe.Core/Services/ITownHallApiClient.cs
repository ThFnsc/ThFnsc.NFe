using System;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Models;
using ThFnsc.NFe.Core.Entities;

namespace ThFnsc.NFe.Core.Services
{
    public interface ITownHallApiClient
    {
        Task<TownHallResponse> GenerateAsync(IssuedNFe nfe);

        public Type DataType { get; }
    }
}
