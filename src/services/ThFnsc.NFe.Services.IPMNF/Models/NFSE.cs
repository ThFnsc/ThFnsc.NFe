using System.Xml.Serialization;

namespace ThFnsc.NFe.Services.IPMNF.Models;

[XmlRoot("nfse")]
public class NFSE
{
    [XmlElement(ElementName = "nf")]
    public NF NF { get; set; }

    [XmlElement(ElementName = "prestador")]
    public Prestador Prestador { get; set; }

    [XmlElement(ElementName = "tomador")]
    public Tomador Tomador { get; set; }

    [XmlElement(ElementName = "itens")]
    public Items Items { get; set; }
}
