using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Data.Context;
using ThFnsc.NFe.Data.Entities;
using ThFnsc.NFe.Data.Repositories;
using ThFnsc.NFe.Infra.Services;

namespace ThFnsc.NFe.Infra.Applications
{
    public class NFeAppService
    {
        private readonly IEnumerable<ITownHallApiClient> _clients;
        private readonly NFContext _context;
        private readonly SMTPAppService _smtp;

        public NFeAppService(
            IEnumerable<ITownHallApiClient> clients,
            NFContext context,
            SMTPAppService smtp)
        {
            _clients = clients;
            _context = context;
            _smtp = smtp;
        }

        public async Task<IssuedNFe> DeleteAsync(int id)
        {
            var nf = await _context.NFes
                .Active()
                .OfId(id)
                .SingleAsync();
            nf.Delete();
            await _context.SaveChangesAsync();
            return nf;
        }

        public async Task MailToAsync(int nfId, int templateId, IEnumerable<string> additionalAddresses)
        {
            var template = await _context.MailTemplates
                .Active()
                .OfId(templateId)
                .SingleAsync();

            var nf = await _context.NFes
                .OfId(nfId)
                .Include(n => n.Provider.SMTP)
                .Include(n => n.Provider.Issuer.Address)
                .Include(n => n.DocumentTo.Address)
                .SingleAsync();

            using var msXml = new MemoryStream(Encoding.UTF8.GetBytes(nf.ReturnedXMLContent));
            using var msPdf = new MemoryStream(nf.ReturnedPDF);

            await _smtp.SendMailAsync(nf.Provider.SMTP.Id, async msg => msg
                .To(new[] { nf.Provider.SMTP.Account }
                    .Concat(additionalAddresses ?? Array.Empty<string>())
                    .Distinct()
                    .Select(m => new FluentEmail.Core.Models.Address(m, null)))
                .Subject(await RazorRenderer.RenderAsync($"mt-{template.Id}-s", template.Subject, nf))
                .Body(await RazorRenderer.RenderAsync($"mt-{template.Id}-b", template.Body, nf), true)
                .Attach(new FluentEmail.Core.Models.Attachment { Filename = $"NF-{nf.Series}.xml", ContentType = "application/xml", Data = msXml })
                .Attach(new FluentEmail.Core.Models.Attachment { Filename = $"NF-{nf.Series}.pdf", ContentType = "application/pdf", Data = msPdf }));
        }

        public async Task<IssuedNFe> IssueNFeAsync(
            int providerId,
            int toDocumentId,
            float value,
            int serviceId,
            string serviceDescription,
            float aliquotPercentage)
        {
            var provider = await _context.Providers
                .Include(p => p.Issuer)
                    .ThenInclude(i => i.Address)
                .Active()
                .OfId(providerId)
                .SingleAsync();

            var client = _clients.Single(c => c.GetType().FullName == provider.TownHallType);

            var toDocument = await _context.Documents
                .OfId(toDocumentId)
                .Include(d => d.Address)
                .SingleAsync();

            var nf = new IssuedNFe(
                provider: provider,
                serviceId: serviceId,
                serviceDescription: serviceDescription,
                value: value,
                aliquotPercentage: aliquotPercentage,
                documentTo: toDocument);

            _context.Add(nf);

            await _context.SaveChangesAsync();

            try
            {
                var res = await client.GenerateAsync(nf);
                nf.OnReturned(res.Success, res.Error?.Message, res.SentXML, res.RawResponse, res.ReturnedXML, res.ReturnedPDF, res.Series, res.VerificationCode, DateTimeOffset.UtcNow);
            }
            catch (Exception e)
            {
                nf.OnReturned(false, e.Message, null, null, null, null, 0, null, DateTimeOffset.UtcNow);
            }
            finally
            {
                await _context.SaveChangesAsync();
            }
            return nf;
        }
    }
}
