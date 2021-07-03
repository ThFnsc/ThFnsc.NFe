using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Data.Context;
using ThFnsc.NFe.Data.Entities;
using ThFnsc.NFe.Data.Repositories;

namespace ThFnsc.NFe.Infra.Applications
{
    public class NFeAppService
    {
        private readonly IEnumerable<ITownHallApiClient> _clients;
        private readonly NFContext _context;

        public NFeAppService(IEnumerable<ITownHallApiClient> clients, NFContext context)
        {
            _clients = clients;
            _context = context;
        }

        public async Task<(Stream, IssuedNFe)> PDFForIdAsync(int id)
        {
            var nf = await _context.NFes
                .OfId(id)
                .SingleAsync();

            return (HtmlToPDF.ConvertHTMLToPDF(nf.ReturnedContent), nf);
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

            var xml = await client.GenerateXMLAsync(nf);
            _context.Add(nf);
            await _context.SaveChangesAsync();
            try
            {
                var res = await client.GenerateFromXMLAsync(nf, xml);
                if (res.Success)
                    nf.OnSuccess(
                        returnedContent: res.RawResponse,
                        series: res.Series,
                        verificationCode: res.VerificationCode,
                        issuedAt: DateTimeOffset.UtcNow, sentContent: xml);
                else
                    nf.OnError(
                        returnedContent: res.RawResponse,
                        errorMessage: "Could not parse response", sentContent: xml);
                return nf;
            }
            catch (Exception e)
            {
                nf.OnError(
                    returnedContent: null,
                    errorMessage: e.Message, sentContent: xml);
                throw;
            }
            finally
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}
