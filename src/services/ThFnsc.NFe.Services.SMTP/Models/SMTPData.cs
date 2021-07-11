using System;
using System.Collections.Generic;
using System.Security;

namespace ThFnsc.NFe.Services.SMTP.Models
{
    public class SMTPData
    {
        public string[] ExtraRecipients { get; set; } = Array.Empty<string>();

        public string RazorSubject { get; set; } = "@using System.Globalization\n Nota fiscal referente a @Model.IssuedAt.AddMonths(-1).ToString(\"MMMM\", new CultureInfo(\"pt-br\"))";

        public string RazorBody { get; set; } = "<p>Em anexo a nota fiscal em forma de PDF e XML</p>\n<p>Essa é uma mensagem automática. Responder se houver qualquer dúvida.</p>";

        public ConnectionModel Connection { get; set; } = new();
    }
}
