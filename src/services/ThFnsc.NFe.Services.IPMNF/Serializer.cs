using System;
using System.Globalization;
using System.Xml.Serialization;
using ThFnsc.NFe.Core.Entities;
using ThFnsc.NFe.Services.IPMNF.Models;

namespace ThFnsc.NFe.Services.IPMNF;

internal static class Serializer
{
    private static readonly CultureInfo _culture = new("pt-br");

    private static string ToCurrency(this float value) =>
        value.ToString("0.00", _culture);


    public static string GenerateNFe(Document from, Document to, float value, int serviceId, string serviceDescription, float aliquotPercentage, string identifier)
    {
        var nfse = new NFSE
        {
            Identificador = identifier,
            NF = new()
            {
                Data = DateTime.Today.ToString("dd/MM/yyyy"),
                ValorTotal = value.ToCurrency(),
                ValorDesconto = 0f.ToCurrency(),
                ValorIr = 0f.ToCurrency(),
                ValorINSS = 0f.ToCurrency(),
                ValorContribuicaoSocial = 0f.ToCurrency(),
                ValorRps = string.Empty,
                ValorPis = 0f.ToCurrency(),
                ValorCofins = 0f.ToCurrency(),
                Observacao = string.Empty
            },
            Prestador = new()
            {
                Cidade = from.Address.CityId,
                CPFCPNJ = from.DocIdentifier
            },
            Tomador = new()
            {
                Tipo = to.DocType.ToUpper() switch
                {
                    "CNPJ" => "J",
                    "CPF" => "F",
                    _ => throw new NotSupportedException(to.DocIdentifier)
                },
                CPFCNPJ = to.DocIdentifier,
                IE = string.Empty,
                NomeRazaoSocial = to.Name,
                SobrenomeNomeFantasia = to.Name,
                Logradouro = to.Address.Street,
                Email = to.Email,
                Complemento = to.Address.Complement,
                PontoReferencia = string.Empty,
                Bairro = to.Address.Neighborhood,
                Cidade = to.Address.CityId,
                CEP = to.Address.PostalCode,
                DDDFoneComercial = string.Empty,
                DDDFoneResidencial = string.Empty,
                DDDFax = string.Empty,
                FoneFax = string.Empty,
                FoneComercial = string.Empty,
                FoneResidencial = string.Empty
            },
            Items = new()
            {
                Itens = new Item[]
                {
                    new()
                    {
                        CodigoLocalPrestacaoServico=from.Address.CityId,
                        CodigoItemListaServico=serviceId,
                        Descritivo = serviceDescription,
                        AliquotaItemListaServico=aliquotPercentage.ToCurrency(),
                        SituacaoTributaria=0,
                        ValorTributavel=value.ToCurrency(),
                        ValorDeducao=0f.ToCurrency(),
                        ValorIssrf=0f.ToCurrency(),
                        TributaMunicipioPrestador="S",
                        UnidadeCodigo=string.Empty,
                        UnidadeQuantidade=string.Empty,
                        UnidadeValorUnitario=string.Empty
                    }
                }
            }
        };

        return SerializeXML(nfse);
    }

    public static string SerializeXML<T>(T obj)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var st = new CustomStringWriter();
        serializer.Serialize(st, obj);
        return st.ToString();
    }
}