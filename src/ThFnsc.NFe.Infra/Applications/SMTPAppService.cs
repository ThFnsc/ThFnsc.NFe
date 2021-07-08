using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ThFnsc.NFe.Data.Context;
using ThFnsc.NFe.Data.Repositories;

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

        public static async Task TestSMTPAsync(
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

        public async Task SendMailAsync(int smtpId, Func<IFluentEmail, Task<IFluentEmail>> content)
        {
            var smtp = await _context.SMTPs
                .Active()
                .OfId(smtpId)
                .SingleAsync();

            var res = await new SmtpSender(
                 new SmtpClient(smtp.Host, smtp.Port)
                 {
                     Credentials = new NetworkCredential(smtp.Username, smtp.Password),
                     EnableSsl = smtp.UseEncryption
                 })
            .SendAsync(await content(Email.From(smtp.Account, smtp.AccountName)));

            if (!res.Successful)
                throw new Exception(string.Join(", ", res.ErrorMessages));
        }
    }
}
