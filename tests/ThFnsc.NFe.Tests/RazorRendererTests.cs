using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using ThFnsc.NFe.Services.RazorEngineRenderer;

namespace ThFnsc.NFe.Tests;

[TestClass]
public class RazorRendererTests
{
    [TestMethod]
    public async Task EnsureRendersCorrectly()
    {
        var result = await new RazorRenderer()
            .RenderAsync(
                templateKey: "test",
                template: "<h1>My name is @Model.Name and I was born in @Model.BirthDate.ToString(\"dd/MM/yyyy\")</h1>",
                model: new
                {
                    Name = "Thiago",
                    BirthDate = new DateTime(2000, 4, 24)
                });

        Assert.AreEqual("<h1>My name is Thiago and I was born in 24/04/2000</h1>", result);
    }
}
