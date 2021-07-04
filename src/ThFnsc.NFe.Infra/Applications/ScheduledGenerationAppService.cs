using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ThFnsc.NFe.Data.Context;
using ThFnsc.NFe.Data.Entities;
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
                .Include(s => s.MailTemplate)
                .Include(s => s.ToDocument)
                .Include(s => s.Provider)
                .SingleAsync();

            throw new NotImplementedException($"{id} Still experimental");
            var nfe = await _nfe.IssueNFeAsync(sg.Provider.Id, sg.ToDocument.Id, sg.Value, sg.ServiceId, sg.ServiceDescription, sg.AliquotPercentage);
            if (!nfe.Success.Value)
                throw new Exception(nfe.ErrorMessage);
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
            string mailList,
            int? mailTemplateId,
            int toDocumentId,
            float value,
            float aliquotPercentage,
            int serviceId,
            string serviceDescription,
            bool enabled)
        {
            var toDoc = await _context.Documents.Active().OfId(toDocumentId).SingleAsync();
            var template = mailTemplateId.HasValue
                ? await _context.MailTemplates.Active().OfId(mailTemplateId.Value).SingleAsync()
                : null;
            var provider = await _context.Providers.Active().OfId(providerId).SingleAsync();

            var scheduledGeneration = new ScheduledGeneration(
                cronPattern,
                provider,
                mailList,
                template,
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
            string mailList,
            int? mailTemplateId,
            int toDocumentId,
            float value,
            float aliquotPercentage,
            int serviceId,
            string serviceDescription,
            bool enabled)
        {
            var scheduledGeneration = await _context.ScheduledGenerations.Active().OfId(id).SingleAsync();
            var toDoc = await _context.Documents.Active().OfId(toDocumentId).SingleAsync();
            var template = mailTemplateId.HasValue
                ? await _context.MailTemplates.Active().OfId(mailTemplateId.Value).SingleAsync()
                : null;
            var provider = await _context.Providers.Active().OfId(providerId).SingleAsync();

            scheduledGeneration.Update(
                cronPattern,
                provider,
                mailList,
                template,
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
