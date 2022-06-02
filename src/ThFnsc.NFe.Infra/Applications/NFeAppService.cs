using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Entities;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Data.Context;
using ThFnsc.NFe.Data.Repositories;

namespace ThFnsc.NFe.Infra.Applications;

public class NFeAppService
{
    private readonly IEnumerable<ITownHallApiClient> _clients;
    private readonly NFContext _context;

    public NFeAppService(
        IEnumerable<ITownHallApiClient> clients,
        NFContext context)
    {
        _clients = clients;
        _context = context;
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
            nf.OnReturned(
                success: res.Success,
                errorMessage: res.Error?.Message,
                sentContent: res.SentXML,
                contentReceivedRaw: res.RawResponse,
                returnedXMLContent: res.ReturnedXML,
                linkToNF: res.LinkNFSE,
                returnedPDF: null,
                series: res.Series,
                verificationCode: res.VerificationCode,
                issuedAt: DateTimeOffset.UtcNow);
        }
        catch (Exception e)
        {
            nf.OnReturned(
                success: false,
                errorMessage: $"{e.Message}\n{e.StackTrace}",
                sentContent: null,
                contentReceivedRaw: null,
                returnedXMLContent: null,
                returnedPDF: null,
                linkToNF: null,
                series: 0,
                verificationCode: null,
                issuedAt: DateTimeOffset.UtcNow);
        }
        finally
        {
            await _context.SaveChangesAsync();
        }
        return nf;
    }
}
