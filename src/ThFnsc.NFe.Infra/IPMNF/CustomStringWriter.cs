using System.IO;
using System.Text;

namespace ThFnsc.NFe.Infra.IPMNF
{
    public class CustomStringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.GetEncoding("ISO-8859-1");
    }
}
