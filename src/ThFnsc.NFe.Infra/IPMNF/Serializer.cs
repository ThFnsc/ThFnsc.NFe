﻿using System;
using System.Globalization;
using System.Xml.Serialization;
using ThFnsc.NFe.Data.Entities;
using ThFnsc.NFe.Infra.IPMNF.Models;

namespace ThFnsc.NFe.Infra.IPMNF
{
    public static class Serializer
    {
        private static readonly CultureInfo _culture = new CultureInfo("pt-br");

        private static string ToCurrency(this float value) =>
            value.ToString("0.00", _culture);


        public static string GenerateNFe(Document from, Document to, float value, int serviceId, string serviceDescription, float aliquotPercentage)
        {
            var nfse = new NFSE
            {
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
                    Cidade = to.Address.City,
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

            var serializer = new XmlSerializer(typeof(NFSE));
            using var st = new CustomStringWriter();
            serializer.Serialize(st, nfse);
            return st.ToString();
        }
    }
}