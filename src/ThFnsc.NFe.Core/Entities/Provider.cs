using System;
using ThFnsc.NFe.Core.Entities.Shared;

namespace ThFnsc.NFe.Core.Entities
{
    public class Provider : BaseSoftDeleteEntity
    {
        public Document Issuer { get; private set; }

        public string Data { get; private set; }

        public string TownHallType { get; private set; }

        public Provider(Document issuer, string data, string townHallType)
        {
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            Data = data ?? throw new ArgumentNullException(nameof(data));
            TownHallType = townHallType ?? throw new ArgumentNullException(nameof(townHallType));
        }

        protected Provider() { }
    }
}
