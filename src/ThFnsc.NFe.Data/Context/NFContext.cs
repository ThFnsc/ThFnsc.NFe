using Microsoft.EntityFrameworkCore;
using ThFnsc.NFe.Core.Entities;

namespace ThFnsc.NFe.Data.Context;

public class NFContext : DbContext
{
    public DbSet<IssuedNFe> NFes { get; private set; }

    public DbSet<Document> Documents { get; private set; }

    public DbSet<Address> Addresses { get; private set; }

    public DbSet<Provider> Providers { get; private set; }

    public DbSet<ScheduledGeneration> ScheduledGenerations { get; private set; }

    public DbSet<NFNotifier> NFNotifiers { get; private set; }

    public NFContext(DbContextOptions<NFContext> options) : base(options) { }
}
