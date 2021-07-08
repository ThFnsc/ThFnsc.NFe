using ThFnsc.NFe.Models.Document;
using ThFnsc.NFe.Models.MailTemplate;
using ThFnsc.NFe.Models.Provider;

namespace ThFnsc.NFe.Models.ScheduledGeneration
{
    public class ScheduledGenerationModel
    {
        public int Id { get; set; }

        public string CronPattern { get; set; }

        public ProviderModel Provider { get; set; }

        public string MailList { get; set; }

        public MailTemplateModel MailTemplate { get; set; }

        public DocumentModel ToDocument { get; set; }

        public float Value { get; set; }

        public float AliquotPercentage { get; set; }

        public int ServiceId { get; set; }

        public string ServiceDescription { get; set; }

        public bool Enabled { get; set; }
    }
}
