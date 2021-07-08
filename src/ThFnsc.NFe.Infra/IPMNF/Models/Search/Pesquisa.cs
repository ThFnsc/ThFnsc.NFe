using System.Xml.Serialization;

namespace ThFnsc.NFe.Infra.IPMNF.Models.Search
{
    public class Pesquisa
    {
        [XmlElement(ElementName = "codigo_autenticidade")]
        public string CodAutent { get; set; }
    }
}
