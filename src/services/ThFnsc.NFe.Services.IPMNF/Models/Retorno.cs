using System;
using System.Xml.Serialization;

namespace ThFnsc.NFe.Services.IPMNF.Models;

[XmlRoot("retorno")]
public class Retorno
{
    [XmlElement("nfse")]
    public NFSERetorno NFSE { get; set; }

    public void Validate()
    {
        ArgumentNullException.ThrowIfNull(NFSE);
        NFSE.Validate();
    }
}

public class NFSERetorno
{
    [XmlElement("nf")]
    public NFRetorno NF { get; set; }

    public void Validate()
    {
        ArgumentNullException.ThrowIfNull(NF);
        NF.Validate();
    }
}

public class NFRetorno
{
    [XmlElement("numero_nfse")]
    public int NumeroNFSE { get; set; }

    [XmlElement("cod_verificador_autenticidade")]
    public string CodVerif { get; set; }

    [XmlElement("link_nfse")]
    public string LinkNFSE { get; set; }

    public void Validate()
    {
        if (NumeroNFSE <= 0)
            throw new ArgumentOutOfRangeException(nameof(NumeroNFSE));
        if (string.IsNullOrWhiteSpace(LinkNFSE))
            throw new ArgumentException(nameof(LinkNFSE));
        if (string.IsNullOrWhiteSpace(CodVerif))
            throw new ArgumentException(nameof(CodVerif));
    }
}