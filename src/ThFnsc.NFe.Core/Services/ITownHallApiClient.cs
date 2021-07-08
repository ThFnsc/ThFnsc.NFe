using System;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Models;
using ThFnsc.NFe.Data.Entities;

namespace ThFnsc.NFe.Core.Services
{
    public interface ITownHallApiClient
    {
        Task<TownHallResponse> GenerateAsync(IssuedNFe nfe);

        public Type ModelType { get; }

        public object DefaultModelData { get; }
    }
}
