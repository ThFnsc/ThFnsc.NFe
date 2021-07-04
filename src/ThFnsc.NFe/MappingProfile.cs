using AutoMapper;
using ThFnsc.NFe.Data.Entities;
using ThFnsc.NFe.Models;

namespace ThFnsc.NFe
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            SMTPMappings();
        }

        private void SMTPMappings()
        {
            CreateMap<SMTP, EditSMTPModel>();
            CreateMap<SMTP, SMTPModel>();
        }
    }
}
