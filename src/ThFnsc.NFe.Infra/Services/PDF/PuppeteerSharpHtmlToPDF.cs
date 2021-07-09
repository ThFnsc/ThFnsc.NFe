using PuppeteerSharp;
using System.Linq;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Services;

namespace ThFnsc.NFe.Infra.Services
{
    public class PuppeteerSharpHtmlToPDF : IHtmlToPDF
    {
        private readonly IChromePathFinder _chromePathFinder;

        public PuppeteerSharpHtmlToPDF(IChromePathFinder chromePathFinder)
        {
            _chromePathFinder = chromePathFinder;
        }

        public async Task<byte[]> ConvertHTMLToPDF(string html)
        {
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new[] { "--no-sandbox" },
                ExecutablePath = await _chromePathFinder.FindChromeAsync()
            });
            var page = (await browser.PagesAsync()).FirstOrDefault() ?? await browser.NewPageAsync();
            await page.SetContentAsync(html);
            var pdf = await page.PdfDataAsync();
            await page.CloseAsync();
            await page.DisposeAsync();
            return pdf;
        }
    }
}
