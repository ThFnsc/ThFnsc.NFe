using System.Xml.Serialization;

namespace ThFnsc.NFe.Services.IPMNF.Models;

public class NF
{
    [XmlElement(ElementName = "data")]
    public string Data { get; set; }

    [XmlElement(ElementName = "valor_total")]
    public string ValorTotal { get; set; }

    [XmlElement(ElementName = "valor_desconto")]
    public string ValorDesconto { get; set; }

    [XmlElement(ElementName = "valor_ir")]
    public string ValorIr { get; set; }

    [XmlElement(ElementName = "valor_inss")]
    public string ValorINSS { get; set; }

    [XmlElement(ElementName = "valor_contribuicao_social")]
    public string ValorContribuicaoSocial { get; set; }

    [XmlElement(ElementName = "valor_rps")]
    public string ValorRps { get; set; }

    [XmlElement(ElementName = "valor_pis")]
    public string ValorPis { get; set; }

    [XmlElement(ElementName = "valor_cofins")]
    public string ValorCofins { get; set; }

    [XmlElement(ElementName = "observacao")]
    public string Observacao { get; set; }
}
