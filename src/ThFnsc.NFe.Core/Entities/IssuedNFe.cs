using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using ThFnsc.NFe.Core.Entities.Shared;

namespace ThFnsc.NFe.Core.Entities;

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

    public string LinkToNF { get; private set; }
    
    public string ReturnedContent { get; private set; }

    public string ReturnedXMLContent { get; private set; }

    [Column(TypeName = "MEDIUMBLOB")]
    public byte[] ReturnedPDF { get; private set; } = Array.Empty<byte>();

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
        IssuedAt = CreatedAt;
    }

    public void OnReturned(
        bool success,
        string errorMessage,
        string sentContent,
        string contentReceivedRaw,
        string returnedXMLContent,
        byte[] returnedPDF,
        string linkToNF,
        int series,
        string verificationCode,
        DateTimeOffset issuedAt)
    {
        if (Success.HasValue)
            throw new InvalidOperationException();
        Success = success;
        ErrorMessage = errorMessage;
        SentContent = sentContent;
        ReturnedContent = contentReceivedRaw;
        ReturnedXMLContent = returnedXMLContent;
        ReturnedPDF = returnedPDF;
        Series = series;
        VerificationCode = verificationCode;
        IssuedAt = issuedAt;
        LinkToNF = linkToNF;
    }

    protected IssuedNFe() { }
}
