using System;
using System.Threading.Tasks;
using ThFnsc.NFe.Data.Entities;

namespace ThFnsc.NFe.Core.Services
{
    public interface INFNotifier
    {
        Type DataType { get; }

        Task NotifyAsync(object data, IssuedNFe nfe);
    }
}
