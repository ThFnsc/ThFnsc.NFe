using HtmlAgilityPack;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using ThFnsc.NFe.Core.Entities;
using ThFnsc.NFe.Core.Models;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Services.IPMNF.Models;

namespace ThFnsc.NFe.Services.IPMNF;

[Display(Name = "IPM Fiscal")]
public class IPMNFApiClient : ITownHallApiClient
{
    private readonly HttpClient _client;
    private readonly IHtmlToPDF _htmlToPDF;

    public Type DataType => typeof(DataModel);

    public IPMNFApiClient(
        HttpClient client,
        IHtmlToPDF htmlToPDF)
    {
        _client = client;
        _htmlToPDF = htmlToPDF;
    }

    private (int series, string verifCode) ParseReturnedHTML(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var series = doc.DocumentNode.SelectSingleNode("//*[contains(text(),'Número da NFS-e')]/parent::*/*[last()]");
        var verifCode = doc.DocumentNode.SelectSingleNode("//*[contains(text(),'Autenticidade')]/parent::*/*[last()]");
        return (int.Parse(series.InnerText.Trim()), verifCode.InnerText.Trim());
    }

    private static string GenerateRequestXML(IssuedNFe nfe) =>
        Serializer.GenerateNFe(
            from: nfe.Provider.Issuer,
            to: nfe.DocumentTo,
            value: nfe.Value,
            serviceId: nfe.ServiceId,
            serviceDescription: nfe.ServiceDescription,
            aliquotPercentage: nfe.AliquotPercentage);

    private async Task<string> PostAsync(string xml, IssuedNFe nfe, string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        var formData = new MultipartFormDataContent();
        var data = JsonSerializer.Deserialize<DataModel>(nfe.Provider.Data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        formData.Add(new StringContent(nfe.Provider.Issuer.DocIdentifier), "login");
        formData.Add(new StringContent(data.Password), "senha");
        formData.Add(new StringContent(nfe.Provider.Issuer.Address.CityId.ToString()), "cidade");
        formData.Add(new StringContent(xml), "f1", "nf.xml");
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
            response = await PostAsync(requestXML, nfe, "http://sync.nfs-e.net/datacenter/include/nfw/importa_nfw/nfw_import_upload.php");
        }
        catch (Exception e)
        {
            return new TownHallResponse { Error = e, SentXML = requestXML, RawResponse = response };
        }

        return await ProcessResponseAsync(requestXML, response, nfe);
    }

    public async Task<TownHallResponse> ProcessResponseAsync(string sent, string received, IssuedNFe nfe)
    {
        var response = new TownHallResponse
        {
            SentXML = sent,
            RawResponse = received
        };

        try
        {
            response.ReturnedPDF = await _htmlToPDF.ConvertHTMLToPDF(response.RawResponse);
        }
        catch (Exception e)
        {
            response.Error = e;
            return response;
        }

        try
        {
            (response.Series, response.VerificationCode) = ParseReturnedHTML(response.RawResponse);
        }
        catch (Exception e)
        {
            response.Error = e;
            return response;
        }

        try
        {
            response.ReturnedXML = await PostAsync(GetXMLXML(response.VerificationCode), nfe, "http://sync.nfs-e.net/datacenter/include/nfw/importa_nfw/nfw_import_upload.php?formato_saida=2&eletron=1");
        }
        catch (Exception e)
        {
            response.Error = e;
            return response;
        }

        response.Success = true;
        return response;
    }

    private static string GetXMLXML(string verificationCode) =>
        Serializer.SerializeXML(new Models.Search.NFSE
        {
            Pesquisa = new Models.Search.Pesquisa
            {
                CodAutent = verificationCode
            }
        });
}
