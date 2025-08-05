namespace freelanceProjectEgypt03.data
{
    using global::freelanceProjectEgypt03.Models;
    using Microsoft.EntityFrameworkCore;

    namespace freelanceProjectEgypt03.Data
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<Client> Clients { get; set; }
            public DbSet<Partner> Partners { get; set; }
            public DbSet<Service> Services { get; set; }
            public DbSet<ContactUs> ContactUsMessages { get; set; }
            public DbSet<DemandeDeService> DemandeDeServices { get; set; }
            public DbSet<FileAttachment> files { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // ✅ DemandeDeService → Client (many-to-one)
                modelBuilder.Entity<DemandeDeService>()
                    .HasOne(d => d.client)
                    .WithMany(c => c.demandeDeServices)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Cascade);

                // ✅ DemandeDeService → Service (many-to-one)
                modelBuilder.Entity<DemandeDeService>()
                    .HasOne(d => d.service)
                    .WithMany(s => s.demandeDeServices)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);

                // ✅ ServiceFile → Service (many-to-one)
                modelBuilder.Entity<FileAttachment>()
                    .HasOne(f => f.Service)
                    .WithMany(s => s.Files)
                    .HasForeignKey(f => f.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);

                // ✅ Handle List<string> in DemandeDeService (stored as string)
                modelBuilder.Entity<DemandeDeService>()
                    .Property(d => d.AdditionalServices)
                    .HasConversion(
                        v => string.Join(';', v),
                        v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                    );

                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
