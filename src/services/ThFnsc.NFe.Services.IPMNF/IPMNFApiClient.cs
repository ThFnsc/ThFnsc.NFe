using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ThFnsc.NFe.Core.Entities;
using ThFnsc.NFe.Core.Models;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Services.IPMNF.Models;

namespace ThFnsc.NFe.Services.IPMNF;

[Display(Name = "IPM Fiscal")]
public class IPMNFApiClient : ITownHallApiClient
{
    private readonly HttpClient _client;

    public Type DataType => typeof(DataModel);

    public IPMNFApiClient(HttpClient client)
    {
        _client = client;
    }

    private static Retorno ParseReturnedXML(string xml)
    {
        var serializer = new XmlSerializer(typeof(Retorno));
        var obj = (Retorno) serializer.Deserialize(new StringReader(xml));
        obj.Validate();
        return obj;
    }

    private static string GenerateRequestXML(IssuedNFe nfe) =>
        Serializer.GenerateNFe(
            from: nfe.Provider.Issuer,
            to: nfe.DocumentTo,
            value: nfe.Value,
            serviceId: nfe.ServiceId,
            serviceDescription: nfe.ServiceDescription,
            aliquotPercentage: nfe.AliquotPercentage,
            identifier: nfe.CreatedAt.ToString("MM_yyyy"));

    private async Task<string> PostAsync(string xml, IssuedNFe nfe, string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        var formData = new MultipartFormDataContent();
        var data = JsonSerializer.Deserialize<DataModel>(nfe.Provider.Data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        request.Headers.Authorization = new BasicAuthenticationHeaderValue(nfe.Provider.Issuer.DocIdentifier, data.Password);

        formData.Add(new StringContent(xml, Encoding.UTF8, "text/xml"), "xml", "arquivo");
        request.Content = formData;

        var res = await _client.SendAsync(request);
        var resBody = await res.Content.ReadAsStringAsync();

        try
        {
            res.EnsureSuccessStatusCode();
            return resBody;
        }
        catch (Exception e)
        {
            e.Data["ResponseText"] = resBody;
            throw;
        }
    }

    public async Task<TownHallResponse> GenerateAsync(IssuedNFe nfe)
    {
        var requestXML = GenerateRequestXML(nfe);
        string response = null;

        try
        {
            response = await PostAsync(requestXML, nfe, "https://ws-gravatai.atende.net:7443/?pg=rest&service=WNERestServiceNFSe&cidade=padrao");
        }
        catch (Exception e)
        {
            return new TownHallResponse { Error = e, SentXML = requestXML, RawResponse = response };
        }

        return ProcessResponseAsync(requestXML, response);
    }

    private static TownHallResponse ProcessResponseAsync(string sent, string received)
    {
        var response = new TownHallResponse
        {
            SentXML = sent,
            ReturnedXML = received
        };

        try
        {
            var resObj = ParseReturnedXML(received);
            var nf = resObj.NFSE.NF;
            response.Series = nf.NumeroNFSE;
            response.VerificationCode = nf.CodVerif;
            response.LinkNFSE = nf.LinkNFSE;
        }
        catch (Exception e)
        {
            response.Error = e;
            return response;
        }

        response.Success = true;
        return response;
    }
}
