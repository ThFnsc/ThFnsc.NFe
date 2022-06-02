using PuppeteerSharp;
using System.Linq;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Infra.Services.Chrome;

namespace ThFnsc.NFe.Services.PuppeteerHTMLToPDF;

public class PuppeteerSharpHtmlToPDF : IHtmlToPDF
{
    public async Task<byte[]> ConvertHTMLToPDF(string html)
    {
        var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            Args = new[] { "--no-sandbox" },
            ExecutablePath = await ChromeFinder.FindChromeAsync()
        });
        var page = (await browser.PagesAsync()).FirstOrDefault() ?? await browser.NewPageAsync();
        await page.SetContentAsync(html);
        var pdf = await page.PdfDataAsync();
        await page.CloseAsync();
        await page.DisposeAsync();
        return pdf;
    }
}
