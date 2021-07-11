using System;
using ThFnsc.NFe.Core.Entities.Shared;

namespace ThFnsc.NFe.Core.Entities
{
    public class Provider : BaseSoftDeleteEntity
    {
        public Document Issuer { get; private set; }

        public SMTP SMTP { get; private set; }

        public string Data { get; private set; }

        public string TownHallType { get; private set; }

        public Provider(Document issuer, SMTP sMTP, string data, string townHallType)
        {
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            SMTP = sMTP ?? throw new ArgumentNullException(nameof(sMTP));
            Data = data ?? throw new ArgumentNullException(nameof(data));
            TownHallType = townHallType ?? throw new ArgumentNullException(nameof(townHallType));
        }

        protected Provider() { }
    }
}
