using System.Xml.Serialization;

namespace ThFnsc.NFe.Services.IPMNF.Models;

public class Item
{
    [XmlElement(ElementName = "codigo_local_prestacao_servico")]
    public int CodigoLocalPrestacaoServico { get; set; }

    [XmlElement(ElementName = "codigo_item_lista_servico")]
    public int CodigoItemListaServico { get; set; }

    [XmlElement(ElementName = "descritivo")]
    public string Descritivo { get; set; }

    [XmlElement(ElementName = "aliquota_item_lista_servico")]
    public string AliquotaItemListaServico { get; set; }

    [XmlElement(ElementName = "situacao_tributaria")]
    public int SituacaoTributaria { get; set; }

    [XmlElement(ElementName = "valor_tributavel")]
    public string ValorTributavel { get; set; }

    [XmlElement(ElementName = "valor_deducao")]
    public string ValorDeducao { get; set; }

    [XmlElement(ElementName = "valor_issrf")]
    public string ValorIssrf { get; set; }

    [XmlElement(ElementName = "tributa_municipio_prestador")]
    public string TributaMunicipioPrestador { get; set; }

    [XmlElement(ElementName = "unidade_codigo")]
    public string UnidadeCodigo { get; set; }

    [XmlElement(ElementName = "unidade_quantidade")]
    public string UnidadeQuantidade { get; set; }

    [XmlElement(ElementName = "unidade_valor_unitario")]
    public string UnidadeValorUnitario { get; set; }
}