using EFCore_Api.Db.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Api.Db
{
    public class CodeFirstDemoContext : DbContext
    {
        public CodeFirstDemoContext(DbContextOptions<CodeFirstDemoContext> options) : base(options)
        {
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerInstrument> PlayerInstruments { get; set; }
        public DbSet<InstrumentType> InstrumentTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Instruments)
                .WithOne();

            modelBuilder.Seed();
        }
    }
}
