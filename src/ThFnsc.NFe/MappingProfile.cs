using AutoMapper;
using ThFnsc.NFe.Data.Entities;
using ThFnsc.NFe.Models;
using ThFnsc.NFe.Models.Address;
using ThFnsc.NFe.Models.Provider;

namespace ThFnsc.NFe
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            SMTPMappings();
            DocumentMappings();
            AddressMappings();
            ProviderMappings();
        }

        private void ProviderMappings()
        {
            CreateMap<Provider, ProviderModel>();
            CreateMap<Provider, EditProviderModel>()
                .ForMember(m => m.SMTPId, res => res.MapFrom(m => m.SMTP.Id))
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

        private void SMTPMappings()
        {
            CreateMap<SMTP, EditSMTPModel>();
            CreateMap<SMTP, SMTPModel>();
        }
    }
}
