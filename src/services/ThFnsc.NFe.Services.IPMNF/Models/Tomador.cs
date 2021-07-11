using System.Xml.Serialization;

namespace ThFnsc.NFe.Services.IPMNF.Models
{
    public class Tomador
    {
        [XmlElement(ElementName = "tipo")]
        public string Tipo { get; set; }

        [XmlElement(ElementName = "cpfcnpj")]
        public string CPFCNPJ { get; set; }

        [XmlElement(ElementName = "ie")]
        public string IE { get; set; }

        [XmlElement(ElementName = "nome_razao_social")]
        public string NomeRazaoSocial { get; set; }

        [XmlElement(ElementName = "sobrenome_nome_fantasia")]
        public string SobrenomeNomeFantasia { get; set; }

        [XmlElement(ElementName = "logradouro")]
        public string Logradouro { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "complemento")]
        public string Complemento { get; set; }

        [XmlElement(ElementName = "ponto_referencia")]
        public string PontoReferencia { get; set; }

        [XmlElement(ElementName = "bairro")]
        public string Bairro { get; set; }

        [XmlElement(ElementName = "cidade")]
        public string Cidade { get; set; }

        [XmlElement(ElementName = "cep")]
        public string CEP { get; set; }

        [XmlElement(ElementName = "ddd_fone_comercial")]
        public string DDDFoneComercial { get; set; }

        [XmlElement(ElementName = "fone_comercial")]
        public string FoneComercial { get; set; }

        [XmlElement(ElementName = "ddd_fone_residencial")]
        public string DDDFoneResidencial { get; set; }

        [XmlElement(ElementName = "fone_residencial")]
        public string FoneResidencial { get; set; }

        [XmlElement(ElementName = "ddd_fax")]
        public string DDDFax { get; set; }

        [XmlElement(ElementName = "fone_fax")]
        public string FoneFax { get; set; }
    }
}
