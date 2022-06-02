using System.Xml.Serialization;

namespace ThFnsc.NFe.Services.IPMNF.Models;

public class Items
{
    [XmlElement(ElementName = "lista")]
    public Item[] Itens { get; set; }
}
