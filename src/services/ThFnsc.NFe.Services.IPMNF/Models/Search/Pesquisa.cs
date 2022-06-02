using System.Xml.Serialization;

namespace ThFnsc.NFe.Services.IPMNF.Models.Search;

public class Pesquisa
{
    [XmlElement(ElementName = "codigo_autenticidade")]
    public string CodAutent { get; set; }
}
