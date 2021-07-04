using AutoMapper;
using ThFnsc.NFe.Data.Entities;
using ThFnsc.NFe.Models;
using ThFnsc.NFe.Models.Address;

namespace ThFnsc.NFe
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            SMTPMappings();
            DocumentMappings();
            AddressMappings();
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
