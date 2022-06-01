using System;

namespace ThFnsc.NFe.Core.Models
{
    public class TownHallResponse
    {
        public bool Success { get; set; }

        public string ReturnedXML { get; set; }

        public int Series { get; set; }

        public string VerificationCode { get; set; }

        public string SentXML { get; set; }

        public Exception Error { get; set; }

        public string LinkNFSE { get; set; }

        public string RawResponse { get; set; }
    }
}
