using Microsoft.EntityFrameworkCore;
using System;
using ThFnsc.NFe.Data.Entities.Shared;

namespace ThFnsc.NFe.Data.Entities
{
    [Index(nameof(Enabled))]
    public class ScheduledGeneration : BaseSoftDeleteEntity
    {
        public string CronPattern { get; private set; }

        public Provider Provider { get; private set; }

        public string MailList { get; private set; }

        public MailTemplate MailTemplate { get; private set; }

        public Document ToDocument { get; private set; }

        public float Value { get; private set; }

        public float AliquotPercentage { get; private set; }

        public int ServiceId { get; private set; }

        public string ServiceDescription { get; private set; }

        public bool Enabled { get; private set; }

        public ScheduledGeneration(
            string cronPattern,
            Provider provider,
            string mailList,
            MailTemplate mailTemplate,
            Document toDocument,
            float value,
            float aliquotPercentage,
            int serviceId,
            string serviceDescription,
            bool enabled) =>
            Update(cronPattern, provider, mailList, mailTemplate, toDocument, value, aliquotPercentage, serviceId, serviceDescription, enabled);

        public void Update(
            string cronPattern,
            Provider provider,
            string mailList,
            MailTemplate mailTemplate,
            Document toDocument,
            float value,
            float aliquotPercentage,
            int serviceId,
            string serviceDescription,
            bool enabled)
        {
            CronPattern = cronPattern ?? throw new ArgumentNullException(nameof(cronPattern));
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            MailList = mailList;
            MailTemplate = mailTemplate;
            ToDocument = toDocument ?? throw new ArgumentNullException(nameof(toDocument));
            Value = value;
            AliquotPercentage = aliquotPercentage;
            ServiceId = serviceId;
            ServiceDescription = serviceDescription ?? throw new ArgumentNullException(nameof(serviceDescription));
            SetEnabled(enabled);
        }

        public void SetEnabled(bool enabled)
        {
            Enabled = enabled;
        }

        protected ScheduledGeneration() { }
    }
}
