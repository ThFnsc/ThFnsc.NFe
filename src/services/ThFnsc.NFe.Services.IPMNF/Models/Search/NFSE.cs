using System.Xml.Serialization;

namespace ThFnsc.NFe.Services.IPMNF.Models.Search;

[XmlRoot("nfse")]
public class NFSE
{
    [XmlElement(ElementName = "pesquisa")]
    public Pesquisa Pesquisa { get; set; }
}
