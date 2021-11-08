using ThFnsc.NFe.Models.Document;
using ThFnsc.NFe.Models.Provider;

namespace ThFnsc.NFe.Models.NFe
{
    public class NFModel
    {
        public int Id { get; set; }

        public ProviderModel Provider { get; set; }

        public int Series { get; set; }

        public string VerificationCode { get; set; }

        public DateTimeOffset IssuedAt { get; set; }

        public DocumentModel DocumentTo { get; set; }

        public bool? Success { get; set; }

        public string ErrorMessage { get; set; }

        public int ServiceId { get; set; }

        public string ServiceDescription { get; set; }

        public float Value { get; set; }

        public float AliquotPercentage { get; set; }
    }
}
