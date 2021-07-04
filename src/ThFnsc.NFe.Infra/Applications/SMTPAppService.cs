using FluentEmail.Core;
using FluentEmail.Smtp;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ThFnsc.NFe.Data.Context;

namespace ThFnsc.NFe.Infra.Applications
{
    public class SMTPAppService
    {
        private readonly NFContext _context;

        public SMTPAppService(
            NFContext context)
        {
            _context = context;
        }

        public async Task TestSMTP(
            string host,
            int port,
            bool useEncryption,
            string account,
            string username,
            string password,
            string accountName)
        {
            var res = await new SmtpSender(
                 new SmtpClient(host, port)
                 {
                     Credentials = new NetworkCredential(username, password),
                     EnableSsl = useEncryption
                 })
                 .SendAsync(
                     Email.From(account, accountName)
                     .To(account, accountName)
                     .Subject("Teste de envio SMTP de ThFnsc.NFe")
                     .Body("Se você recebeu essa mensagem, significa que deu tudo certo!"));
            if (!res.Successful)
                throw new Exception(string.Join(", ", res.ErrorMessages));
        }
    }
}
