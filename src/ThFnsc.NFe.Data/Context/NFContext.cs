using Microsoft.EntityFrameworkCore;
using ThFnsc.NFe.Data.Entities;

namespace ThFnsc.NFe.Data.Context
{
    public class NFContext : DbContext
    {
        public DbSet<IssuedNFe> NFes { get; private set; }

        public DbSet<Document> Documents { get; private set; }

        public DbSet<Address> Addresses { get; private set; }

        public DbSet<SMTP> SMTPs { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<MailTemplate> MailTemplates { get; set; }

        public NFContext(DbContextOptions<NFContext> options) : base(options) { }
    }
}
