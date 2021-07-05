using SelectPdf;
using System.IO;

namespace ThFnsc.NFe.Infra.Services
{
    public static class HtmlToPDF
    {
        public static MemoryStream ConvertHTMLToPDF(string html)
        {
            var converter = new HtmlToPdf();
            var doc = converter.ConvertHtmlString(html);
            var ms = new MemoryStream();
            doc.Save(ms);
            doc.Close();
            ms.Position = 0;
            return ms;
        }
    }
}
