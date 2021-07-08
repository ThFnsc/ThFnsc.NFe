using ThFnsc.NFe.Models.Document;
using ThFnsc.NFe.Models.SMTP;

namespace ThFnsc.NFe.Models.Provider
{
    public class ProviderModel
    {
        public int Id { get; set; }

        public DocumentModel Issuer { get; set; }

        public SMTPModel SMTP { get; set; }

        public string Data { get; set; }

        public string TownHallType { get; set; }
    }
}
