using System.Xml.Serialization;

namespace ThFnsc.NFe.Infra.IPMNF.Models
{
    public class Items
    {
        [XmlElement(ElementName = "lista")]
        public Item[] Itens { get; set; }
    }
}
