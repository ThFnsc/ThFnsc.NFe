using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Entities;
using ThFnsc.NFe.Data.Context;
using ThFnsc.NFe.Data.Repositories;

namespace ThFnsc.NFe.Infra.Applications
{
    [AutomaticRetry(Attempts = 0)]
    public class ScheduledGenerationAppService
    {
        private readonly NFContext _context;
        private readonly NFeAppService _nfe;

        public ScheduledGenerationAppService(
            NFContext context,
            NFeAppService nfe)
        {
            _context = context;
            _nfe = nfe;
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
                BackgroundJob.Schedule<NotificationAppService>(nas => nas.NotifyAsync(nfe.Id, notifier.Id), notifier.Delay);

            return nfe;
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
            bool enabled,
            IEnumerable<int> notifiers)
        {
            var toDoc = await _context.Documents.Active().OfId(toDocumentId).SingleAsync();
            var provider = await _context.Providers.Active().OfId(providerId).SingleAsync();
            var notifiersOnDb = await _context.NFNotifiers.Where(n => notifiers.Contains(n.Id)).ToListAsync();

            var scheduledGeneration = new ScheduledGeneration(
                cronPattern,
                provider,
                toDoc,
                value,
                aliquotPercentage,
                serviceId,
                serviceDescription,
                enabled);

            foreach (var notifier in notifiersOnDb)
                scheduledGeneration.Notifiers.Add(notifier);

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
            bool enabled,
            IEnumerable<int> notifiers)
        {
            var scheduledGeneration = await _context.ScheduledGenerations.Active().OfId(id).Include(s=>s.Notifiers).SingleAsync();
            var toDoc = await _context.Documents.Active().OfId(toDocumentId).SingleAsync();
            var provider = await _context.Providers.Active().OfId(providerId).SingleAsync();
            var notifiersOnDb = await _context.NFNotifiers.Active().Where(n => notifiers.Contains(n.Id)).ToListAsync();

            scheduledGeneration.Update(
                cronPattern,
                provider,
                toDoc,
                value,
                aliquotPercentage,
                serviceId,
                serviceDescription,
                enabled);

            scheduledGeneration.Notifiers.Clear();
            foreach (var notifier in notifiersOnDb)
                scheduledGeneration.Notifiers.Add(notifier);

            await _context.SaveChangesAsync();
            UpdateRecurringJob(scheduledGeneration);
            return scheduledGeneration;
        }
    }
}
