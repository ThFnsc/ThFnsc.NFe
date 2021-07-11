using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Data.Context;
using ThFnsc.NFe.Core.Entities;
using ThFnsc.NFe.Data.Repositories;

namespace ThFnsc.NFe.Infra.Applications
{
    [AutomaticRetry(Attempts = 0)]
    public class ScheduledGenerationAppService
    {
        private readonly NFContext _context;
        private readonly NFeAppService _nfe;
        private readonly IEnumerable<INFNotifier> _nfNotifiers;

        public ScheduledGenerationAppService(
            NFContext context,
            NFeAppService nfe,
            IEnumerable<INFNotifier> nfNotifiers)
        {
            _context = context;
            _nfe = nfe;
            _nfNotifiers = nfNotifiers;
        }

        public async Task<IssuedNFe> ExecuteSchedule(int id)
        {
            var sg = await _context.ScheduledGenerations
                .Active()
                .OfId(id)
                .Where(s => s.Enabled)
                .Include(s => s.ToDocument)
                .Include(s => s.Provider)
                .Include(s => s.Notifiers)
                .SingleAsync();

            var nfe = await _nfe.IssueNFeAsync(sg.Provider.Id, sg.ToDocument.Id, sg.Value, sg.ServiceId, sg.ServiceDescription, sg.AliquotPercentage);

            if (!nfe.Success.Value)
                throw new Exception(nfe.ErrorMessage);

            foreach (var notifier in sg.Notifiers)
                BackgroundJob.Schedule(() => NotifyAsync(nfe.Id, notifier.Id), notifier.Delay);

            return nfe;
        }

        public async Task NotifyAsync(int nfId, int notifierId)
        {
            var nf = await _context.NFes
                .Active()
                .OfId(nfId)
                .Include(n => n.DocumentTo.Address)
                .Include(n => n.Provider.Issuer.Address)
                .Include(n => n.Provider.SMTP)
                .SingleAsync();

            var notifier = await _context.NFNotifiers
                .Active()
                .OfId(notifierId)
                .SingleAsync();

            var notifierService = _nfNotifiers
                .Single(n => n.GetType().FullName == notifier.NotifierType);

            await notifierService.NotifyAsync(
                data: JsonSerializer.Deserialize(
                    json: notifier.JsonData,
                    returnType: notifierService.DataType,
                    options: new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }),
                nfe: nf);
        }

        public async Task<ScheduledGeneration> ToggleEnableAsync(int id)
        {
            var onDb = await _context.ScheduledGenerations
                .Active()
                .OfId(id)
                .SingleAsync();
            onDb.SetEnabled(!onDb.Enabled);
            await _context.SaveChangesAsync();
            UpdateRecurringJob(onDb);
            return onDb;
        }

        public async Task<ScheduledGeneration> DeleteAsync(int id)
        {
            var onDb = await _context.ScheduledGenerations
                .Active()
                .OfId(id)
                .SingleAsync();
            onDb.Delete();
            await _context.SaveChangesAsync();
            UpdateRecurringJob(onDb);
            return onDb;
        }

        private static void UpdateRecurringJob(ScheduledGeneration sg)
        {
            var id = $"ScheduledGeneration={sg.Id}";
            if (sg.Enabled && !sg.DeletedAt.HasValue)
                RecurringJob.AddOrUpdate<ScheduledGenerationAppService>(id, sgApp => sgApp.ExecuteSchedule(sg.Id), sg.CronPattern);
            else
                RecurringJob.RemoveIfExists(id);
        }

        public async Task UpdateAllJobs()
        {
            await foreach (var sg in _context.ScheduledGenerations.AsAsyncEnumerable())
                UpdateRecurringJob(sg);
        }

        public async Task<ScheduledGeneration> CreateAsync(
            string cronPattern,
            int providerId,
            int toDocumentId,
            float value,
            float aliquotPercentage,
            int serviceId,
            string serviceDescription,
            bool enabled)
        {
            var toDoc = await _context.Documents.Active().OfId(toDocumentId).SingleAsync();
            var provider = await _context.Providers.Active().OfId(providerId).SingleAsync();

            var scheduledGeneration = new ScheduledGeneration(
                cronPattern,
                provider,
                toDoc,
                value,
                aliquotPercentage,
                serviceId,
                serviceDescription,
                enabled);

            _context.Add(scheduledGeneration);
            await _context.SaveChangesAsync();
            UpdateRecurringJob(scheduledGeneration);
            return scheduledGeneration;
        }

        public async Task<ScheduledGeneration> UpdateAsync(
            int id,
            string cronPattern,
            int providerId,
            int toDocumentId,
            float value,
            float aliquotPercentage,
            int serviceId,
            string serviceDescription,
            bool enabled)
        {
            var scheduledGeneration = await _context.ScheduledGenerations.Active().OfId(id).SingleAsync();
            var toDoc = await _context.Documents.Active().OfId(toDocumentId).SingleAsync();
            var provider = await _context.Providers.Active().OfId(providerId).SingleAsync();

            scheduledGeneration.Update(
                cronPattern,
                provider,
                toDoc,
                value,
                aliquotPercentage,
                serviceId,
                serviceDescription,
                enabled);

            await _context.SaveChangesAsync();
            UpdateRecurringJob(scheduledGeneration);
            return scheduledGeneration;
        }
    }
}
