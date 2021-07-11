using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Data.Context;
using ThFnsc.NFe.Data.Repositories;

namespace ThFnsc.NFe.Infra.Applications
{
    public class NotificationAppService
    {
        private readonly NFContext _context;
        private readonly IEnumerable<INFNotifier> _nfNotifiers;

        public NotificationAppService(
            NFContext context,
            IEnumerable<INFNotifier> nfNotifiers)
        {
            _context = context;
            _nfNotifiers = nfNotifiers;
        }

        public async Task NotifyAsync(int nfId, int notifierId)
        {
            var nf = await _context.NFes
                .Active()
                .OfId(nfId)
                .Include(n => n.DocumentTo.Address)
                .Include(n => n.Provider.Issuer.Address)
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
    }
}
