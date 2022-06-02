using System.Xml.Serialization;

namespace ThFnsc.NFe.Services.IPMNF.Models;

public class Prestador
{
    [XmlElement(ElementName = "cpfcnpj")]
    public string CPFCPNJ { get; set; }

    [XmlElement(ElementName = "cidade")]
    public int Cidade { get; set; }
}
