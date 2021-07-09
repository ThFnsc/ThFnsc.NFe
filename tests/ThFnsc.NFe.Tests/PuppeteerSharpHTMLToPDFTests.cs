using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using ThFnsc.NFe.Infra.Services;
using ThFnsc.NFe.Infra.Services.Chrome;

namespace ThFnsc.NFe.Tests
{
    [TestClass]
    public class PuppeteerSharpHTMLToPDFTests
    {
        [TestMethod]
        public async Task EnsureGenerates()
        {
            var result = await new PuppeteerSharpHtmlToPDF(new DumbChromeFinder()).ConvertHTMLToPDF("<h1>Hello world!</h1>");
            Assert.IsTrue(result.Length > 0, "Empty PDF");
        }
    }
}
