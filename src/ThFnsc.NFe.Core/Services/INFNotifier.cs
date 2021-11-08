using ThFnsc.NFe.Core.Entities;

namespace ThFnsc.NFe.Core.Services
{
    public interface INFNotifier
    {
        Type DataType { get; }

        Task NotifyAsync(object data, IssuedNFe nfe);
    }
}
