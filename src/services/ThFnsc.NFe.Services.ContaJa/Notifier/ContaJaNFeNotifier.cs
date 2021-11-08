using PuppeteerSharp;
using System.ComponentModel.DataAnnotations;
using ThFnsc.NFe.Core.Entities;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Services.ChromeUtilities;

namespace ThFnsc.NFe.Services.ContaJa.Notifier
{
    [Display(Name = "ContaJá")]
    public class ContaJaNFeNotifier : INFNotifier
    {
        private readonly IRazorRenderer _razorRenderer;

        public ContaJaNFeNotifier(
            IRazorRenderer razorRenderer)
        {
            _razorRenderer = razorRenderer;
        }

        public Type DataType { get; } = typeof(ContaJaNotifierModel);

        public async Task NotifyAsync(object data, IssuedNFe nfe)
        {
            var confs = data as ContaJaNotifierModel;
            //Open a new instance of Chrome
            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new[] { "--no-sandbox" },
                ExecutablePath = await ChromeFinder.FindChromeAsync()
            });

            //Create a temporary file to hold the XML
            var xmlFile = new FileInfo(Path.Join(Path.GetTempPath(), Guid.NewGuid().ToString(), await _razorRenderer.RenderAsync(null, confs.RazorFilename, nfe)));
            xmlFile.Directory.Create();
            try
            {
                //Puts the XML in the file
                await File.WriteAllTextAsync(xmlFile.FullName, nfe.ReturnedXMLContent);

                //Gets the first tab on the browser or creates a new one if needed
                var page = (await browser.PagesAsync()).FirstOrDefault() ?? await browser.NewPageAsync();

                //Go to the login page
                await page.GoToAsync("https://app.contaja.com.br/login", WaitUntilNavigation.DOMContentLoaded);

                //Enter the credentials
                await page.TypeAsync("input[name=\"email\"]", confs.Email);
                await page.TypeAsync("input[name=\"password\"]", confs.Password);

                //Clicks the login button and waits for the request to complete
                await Task.WhenAll(
                    page.ClickAsync("button[type=\"submit\"]"),
                    page.WaitForNavigationAsync(new NavigationOptions { WaitUntil = new[] { WaitUntilNavigation.DOMContentLoaded } }));

                //Navigates to the page to upload the XML
                await page.GoToAsync("https://app.contaja.com.br/notas-fiscais/create", WaitUntilNavigation.DOMContentLoaded);

                //Sets the month and year the NF was generated on
                await page.TypeAsync("input[name=\"competencia\"]", nfe.IssuedAt.ToString("MM/yyyy"));

                //Presses escape to close the datetimepicker. Otherwise the file chooser wont open later
                await page.Keyboard.PressAsync(PuppeteerSharp.Input.Key.Escape);

                //Selects the option to Invoice (index 2)
                await page.SelectAsync("select[name=\"tipo\"]", "2");

                //Defines the invoice description
                await page.TypeAsync("textarea[name=\"descricao\"]", confs.NFDescription);

                //Clicks the upload file button and waits for the file picker to show up
                var fileChooserTask = page.WaitForFileChooserAsync();
                await Task.WhenAll(fileChooserTask, page.ClickAsync("input[name=\"documento\"]"));

                //Choses the XML file created earlier and confirms
                await fileChooserTask.Result.AcceptAsync(xmlFile.FullName);

                //Clicks the submit button and waits for the page to refresh
                await Task.WhenAll(
                    page.ClickAsync("button[type=\"submit\"]"),
                    page.WaitForNavigationAsync(new NavigationOptions { WaitUntil = new[] { WaitUntilNavigation.DOMContentLoaded } }));
            }
            finally
            {
                //Disposes of the temporary file created
                xmlFile.Directory.Delete(recursive: true);
            }
        }
    }
}