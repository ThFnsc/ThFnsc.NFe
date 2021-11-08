using System.Text;

namespace ThFnsc.NFe.Services.IPMNF
{
    internal class CustomStringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.GetEncoding("ISO-8859-1");
    }
}
