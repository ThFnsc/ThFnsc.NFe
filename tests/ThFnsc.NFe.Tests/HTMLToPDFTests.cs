using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using ThFnsc.NFe.Infra.Services;

namespace ThFnsc.NFe.Tests
{
    [TestClass]
    public class HTMLToPDFTests
    {
        [TestMethod]
        public async Task EnsureGenerates()
        {
            var result = await new HtmlToPDF(new NullLogger<HtmlToPDF>()).ConvertHTMLToPDF("<h1>Hello world!</h1>");
            Assert.IsTrue(result.Length > 0, "Empty PDF");
        }
    }
}
