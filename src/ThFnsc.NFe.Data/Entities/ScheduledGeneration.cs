using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ThFnsc.NFe.Data.Entities.Shared;

namespace ThFnsc.NFe.Data.Entities
{
    [Index(nameof(Enabled))]
    public class ScheduledGeneration : BaseSoftDeleteEntity
    {
        public string CronPattern { get; private set; }

        public Provider Provider { get; private set; }

        public Document ToDocument { get; private set; }

        public float Value { get; private set; }

        public float AliquotPercentage { get; private set; }

        public int ServiceId { get; private set; }

        public string ServiceDescription { get; private set; }

        public bool Enabled { get; private set; }

        public ICollection<NFNotifier> Notifiers { get; private set; }

        public ScheduledGeneration(
            string cronPattern,
            Provider provider,
            Document toDocument,
            float value,
            float aliquotPercentage,
            int serviceId,
            string serviceDescription,
            bool enabled)
        {
            Update(cronPattern, provider, toDocument, value, aliquotPercentage, serviceId, serviceDescription, enabled);
            Notifiers = new List<NFNotifier>();
        }

        public void Update(
            string cronPattern,
            Provider provider,
            Document toDocument,
            float value,
            float aliquotPercentage,
            int serviceId,
            string serviceDescription,
            bool enabled)
        {
            CronPattern = cronPattern ?? throw new ArgumentNullException(nameof(cronPattern));
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
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
