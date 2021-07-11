using System.IO;
using System.Text;

namespace ThFnsc.NFe.Services.IPMNF
{
    public class CustomStringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.GetEncoding("ISO-8859-1");
    }
}
