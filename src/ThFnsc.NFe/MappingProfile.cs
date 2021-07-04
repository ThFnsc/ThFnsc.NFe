using AutoMapper;
using ThFnsc.NFe.Data.Entities;
using ThFnsc.NFe.Models;
using ThFnsc.NFe.Models.Address;
using ThFnsc.NFe.Models.MailTemplate;
using ThFnsc.NFe.Models.Provider;
using ThFnsc.NFe.Models.ScheduledGeneration;

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
            MailTemplateMappings();
            ScheduledGenerationMappings();
        }

        private void ScheduledGenerationMappings()
        {
            CreateMap<ScheduledGeneration, ScheduledGenerationModel>();

            CreateMap<IssuedNFe, EditScheduledGenerationModel>()
                .ForMember(m => m.ProviderId, res => res.MapFrom(m => m.Provider.Id))
                .ForMember(m => m.ToDocumentId, res => res.MapFrom(m => m.DocumentTo.Id));

            CreateMap<ScheduledGeneration, EditScheduledGenerationModel>()
                .ForMember(m => m.MailTemplateId, res => res.MapFrom(m => m.MailTemplate.Id))
                .ForMember(m => m.ProviderId, res => res.MapFrom(m => m.Provider.Id))
                .ForMember(m => m.ToDocumentId, res => res.MapFrom(m => m.ToDocument.Id));
        }

        private void MailTemplateMappings()
        {
            CreateMap<MailTemplate, MailTemplateModel>();
            CreateMap<MailTemplate, EditMailTemplateModel>();
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
