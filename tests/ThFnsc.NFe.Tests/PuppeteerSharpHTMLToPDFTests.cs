using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using ThFnsc.NFe.Services.PuppeteerHTMLToPDF;

namespace ThFnsc.NFe.Tests;

[TestClass]
public class PuppeteerSharpHTMLToPDFTests
{
    [TestMethod]
    public async Task EnsureGenerates()
    {
        var result = await new PuppeteerSharpHtmlToPDF().ConvertHTMLToPDF("<h1>Hello world!</h1>");
        Assert.IsTrue(result.Length > 0, "Empty PDF");
    }
}
