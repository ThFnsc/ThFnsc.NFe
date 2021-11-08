using Microsoft.EntityFrameworkCore;
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
            nf.OnReturned(res.Success, res.Error?.Message, res.SentXML, res.RawResponse, res.ReturnedXML, res.ReturnedPDF, res.Series, res.VerificationCode, DateTimeOffset.UtcNow);
        }
        catch (Exception e)
        {
            nf.OnReturned(false, $"{e.Message}\n{e.StackTrace}", null, null, null, null, 0, null, DateTimeOffset.UtcNow);
        }
        finally
        {
            await _context.SaveChangesAsync();
        }
        return nf;
    }
}
