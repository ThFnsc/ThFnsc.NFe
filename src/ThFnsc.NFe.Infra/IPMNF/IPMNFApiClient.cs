using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Models;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Data.Entities;

namespace ThFnsc.NFe.Infra.IPMNF
{
    public class IPMNFApiClient : ITownHallApiClient
    {
        private readonly HttpClient _client;

        public IPMNFApiClient(HttpClient client)
        {
            _client = client;
        }

        private static TownHallResponse ParseFromHTML(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var series = doc.DocumentNode.SelectSingleNode("//*[contains(text(),'Número da NFS-e')]/parent::*/*[last()]");
            var verifCode = doc.DocumentNode.SelectSingleNode("//*[contains(text(),'Autenticidade')]/parent::*/*[last()]");
            var res = new TownHallResponse { RawResponse = html };
            if (series is not null && verifCode is not null)
                if (int.TryParse(series.InnerHtml.Trim(), out var seriesInt))
                {
                    res.Series = seriesInt;
                    res.VerificationCode = verifCode.InnerText.Trim();
                    res.Success = !string.IsNullOrWhiteSpace(res.VerificationCode);
                }
            return res;
        }

        public async Task<TownHallResponse> GenerateFromXMLAsync(IssuedNFe nfe, string xml)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://sync.nfs-e.net/datacenter/include/nfw/importa_nfw/nfw_import_upload.php");
            var formData = new MultipartFormDataContent();
            var data = JsonSerializer.Deserialize<DataModel>(nfe.Provider.Data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            formData.Add(new StringContent(nfe.Provider.Issuer.DocIdentifier), "login");
            formData.Add(new StringContent(data.Password), "senha");
            formData.Add(new StringContent(nfe.Provider.Issuer.Address.CityId.ToString()), "cidade");
            formData.Add(new StringContent(xml), "f1", "nf.xml");
            request.Content = formData;

            var res = await _client.SendAsync(request);
            var resBody = await res.Content.ReadAsStringAsync();

            try { res.EnsureSuccessStatusCode(); }
            catch (Exception e) { e.Data["ResponseText"] = resBody; throw; }

            return ParseFromHTML(resBody);
        }

        public Task<string> GenerateXMLAsync(IssuedNFe nfe) =>
            Task.FromResult(Serializer.GenerateNFe(
                from: nfe.Provider.Issuer,
                to: nfe.DocumentTo,
                value: nfe.Value,
                serviceId: nfe.ServiceId,
                serviceDescription: nfe.ServiceDescription,
                aliquotPercentage: nfe.AliquotPercentage));
    }
}
