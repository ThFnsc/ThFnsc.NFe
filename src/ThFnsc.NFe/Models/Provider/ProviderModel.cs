using ThFnsc.NFe.Models.Document;

namespace ThFnsc.NFe.Models.Provider
{
    public class ProviderModel
    {
        public int Id { get; set; }

        public DocumentModel Issuer { get; set; }

        public string Data { get; set; }

        public string TownHallType { get; set; }
    }
}
