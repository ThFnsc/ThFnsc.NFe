using AutoMapper;
using ThFnsc.NFe.Core.Entities;
using ThFnsc.NFe.Models.Address;
using ThFnsc.NFe.Models.Document;
using ThFnsc.NFe.Models.NFe;
using ThFnsc.NFe.Models.Notifier;
using ThFnsc.NFe.Models.Provider;
using ThFnsc.NFe.Models.ScheduledGeneration;

namespace ThFnsc.NFe;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        NFMappings();
        DocumentMappings();
        AddressMappings();
        ProviderMappings();
        ScheduledGenerationMappings();
        NotifierMappings();
    }

    private void NotifierMappings()
    {
        CreateMap<NFNotifier, NotifierModel>();
        CreateMap<NFNotifier, EditNotifierModel>();
    }

    private void NFMappings()
    {
        CreateMap<IssuedNFe, NFModel>();
        CreateMap<IssuedNFe, GenerateNFModel>()
            .ForMember(m => m.FromId, res => res.MapFrom(m => m.Provider.Id))
            .ForMember(m => m.ToId, res => res.MapFrom(m => m.DocumentTo.Id));
    }

    private void ScheduledGenerationMappings()
    {
        CreateMap<ScheduledGeneration, ScheduledGenerationModel>();

        CreateMap<IssuedNFe, EditScheduledGenerationModel>()
            .ForMember(m => m.ProviderId, res => res.MapFrom(m => m.Provider.Id))
            .ForMember(m => m.ToDocumentId, res => res.MapFrom(m => m.DocumentTo.Id));

        CreateMap<ScheduledGeneration, EditScheduledGenerationModel>()
            .ForMember(m => m.NotifierIDs, res => res.MapFrom(m => m.Notifiers.Select(n => n.Id)))
            .ForMember(m => m.ProviderId, res => res.MapFrom(m => m.Provider.Id))
            .ForMember(m => m.ToDocumentId, res => res.MapFrom(m => m.ToDocument.Id));
    }

    private void ProviderMappings()
    {
        CreateMap<Provider, ProviderModel>();
        CreateMap<Provider, EditProviderModel>()
            .ForMember(m => m.DocumentId, res => res.MapFrom(m => m.Issuer.Id));
    }

    private void AddressMappings()
    {
        CreateMap<Address, AddressModel>();
        CreateMap<Address, EditAddressModel>();
    }

    private void DocumentMappings()
    {
        CreateMap<Document, DocumentModel>();
        CreateMap<Document, EditDocumentModel>();
    }
}
