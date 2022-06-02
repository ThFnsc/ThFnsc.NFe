using System.Collections.Generic;
using ThFnsc.NFe.Models.Document;
using ThFnsc.NFe.Models.Notifier;
using ThFnsc.NFe.Models.Provider;

namespace ThFnsc.NFe.Models.ScheduledGeneration;

public class ScheduledGenerationModel
{
    public int Id { get; set; }

    public string CronPattern { get; set; }

    public ProviderModel Provider { get; set; }

    public DocumentModel ToDocument { get; set; }

    public float Value { get; set; }

    public float AliquotPercentage { get; set; }

    public int ServiceId { get; set; }

    public string ServiceDescription { get; set; }

    public bool Enabled { get; set; }

    public List<NotifierModel> Notifiers { get; set; }
}
