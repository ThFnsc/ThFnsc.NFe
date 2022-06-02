using System;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Entities;
using ThFnsc.NFe.Core.Models;

namespace ThFnsc.NFe.Core.Services;

public interface ITownHallApiClient
{
    Task<TownHallResponse> GenerateAsync(IssuedNFe nfe);

    public Type DataType { get; }
}
