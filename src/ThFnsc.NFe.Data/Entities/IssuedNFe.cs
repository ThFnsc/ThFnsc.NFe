using Microsoft.EntityFrameworkCore;
using System;
using ThFnsc.NFe.Data.Entities.Shared;

namespace ThFnsc.NFe.Data.Entities
{
    [Index(nameof(Series))]
    [Index(nameof(VerificationCode))]
    [Index(nameof(IssuedAt))]
    [Index(nameof(Success))]
    public class IssuedNFe : BaseSoftDeleteEntity
    {
        public Provider Provider { get; private set; }

        public int Series { get; private set; }

        public string VerificationCode { get; private set; }

        public DateTimeOffset IssuedAt { get; private set; }

        public string ReturnedContent { get; private set; }

        public string SentContent { get; private set; }

        public Document DocumentTo { get; private set; }

        public bool? Success { get; private set; }

        public string ErrorMessage { get; private set; }

        public int ServiceId { get; private set; }

        public string ServiceDescription { get; private set; }

        public float Value { get; private set; }

        public float AliquotPercentage { get; private set; }

        public IssuedNFe(Provider provider, int serviceId, string serviceDescription, float value, float aliquotPercentage, Document documentTo)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            ServiceId = serviceId;
            Value = value;
            AliquotPercentage = aliquotPercentage;
            ServiceDescription = serviceDescription ?? throw new ArgumentNullException(nameof(serviceDescription));
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            DocumentTo = documentTo ?? throw new ArgumentNullException(nameof(documentTo));
        }

        public void OnSuccess(string returnedContent, int series, string verificationCode, DateTimeOffset issuedAt, string sentContent)
        {
            if (Success.HasValue)
                throw new InvalidOperationException();
            Series = series;
            VerificationCode = verificationCode ?? throw new ArgumentNullException(nameof(verificationCode));
            IssuedAt = issuedAt;
            ReturnedContent = returnedContent ?? throw new ArgumentNullException(nameof(returnedContent));
            SentContent = sentContent ?? throw new ArgumentNullException(nameof(sentContent));
            Success = true;
        }

        public void OnError(string returnedContent, string errorMessage, string sentContent)
        {
            if (Success.HasValue)
                throw new InvalidOperationException();
            ErrorMessage = errorMessage;
            ReturnedContent = returnedContent;
            SentContent = sentContent ?? throw new ArgumentNullException(nameof(sentContent));
            Success = false;
        }

        protected IssuedNFe() { }
    }
}
