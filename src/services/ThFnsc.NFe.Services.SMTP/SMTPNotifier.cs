using FluentEmail.Core;
using FluentEmail.Smtp;
using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Entities;
using ThFnsc.NFe.Core.Services;
using ThFnsc.NFe.Services.SMTP.Models;
using System.Linq;
using System.IO;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ThFnsc.NFe.Services.SMTP
{
    [Display(Name = "SMTP")]
    public class SMTPNotifier : INFNotifier
    {
        private readonly IRazorRenderer _razorRenderer;

        public SMTPNotifier(IRazorRenderer razorRenderer)
        {
            _razorRenderer = razorRenderer;
        }

        public Type DataType { get; } = typeof(SMTPData);

        public async Task NotifyAsync(object data, IssuedNFe nfe)
        {
            var confs = data as SMTPData;

            using var msXml = new MemoryStream(Encoding.UTF8.GetBytes(nfe.ReturnedXMLContent));
            using var msPdf = new MemoryStream(nfe.ReturnedPDF);

            var res = await new SmtpSender(
                 new SmtpClient(confs.Connection.Host, confs.Connection.Port)
                 {
                     Credentials = new NetworkCredential(confs.Connection.Username, confs.Connection.Password),
                     EnableSsl = confs.Connection.UseEncryption
                 })
            .SendAsync(
                Email.From(confs.Connection.Account, confs.Connection.AccountName)
                    .To(new[] { confs.Connection.Account }
                    .Concat(confs.ExtraRecipients ?? Array.Empty<string>())
                    .Distinct()
                    .Select(m => new FluentEmail.Core.Models.Address(m, null)))
                .Subject(await _razorRenderer.RenderAsync(null, confs.RazorSubject, nfe))
                .Body(await _razorRenderer.RenderAsync(null, confs.RazorBody, nfe), true)
                .Attach(new FluentEmail.Core.Models.Attachment { Filename = $"NF-{nfe.Series}.xml", ContentType = "application/xml", Data = msXml })
                .Attach(new FluentEmail.Core.Models.Attachment { Filename = $"NF-{nfe.Series}.pdf", ContentType = "application/pdf", Data = msPdf }));

            if (!res.Successful)
                throw new Exception(string.Join(", ", res.ErrorMessages));
        }
    }
}
