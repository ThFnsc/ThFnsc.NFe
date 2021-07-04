using Microsoft.EntityFrameworkCore;
using System;
using ThFnsc.NFe.Data.Entities.Shared;

namespace ThFnsc.NFe.Data.Entities
{
    [Index(nameof(Name))]
    public class MailTemplate : BaseSoftDeleteEntity
    {
        public string Name { get; private set; }

        public string Subject { get; private set; }

        public string Body { get; private set; }

        public MailTemplate(string name, string subject, string body) =>
            Update(name, subject, body);

        public void Update(string name, string subject, string body)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            Body = body ?? throw new ArgumentNullException(nameof(body));
        }

        protected MailTemplate() { }
    }
}
