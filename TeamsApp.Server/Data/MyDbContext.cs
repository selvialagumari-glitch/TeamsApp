using Microsoft.EntityFrameworkCore;
using TeamsApp.Shared;

namespace TeamsApp.Server.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<AcknowledgementDto> Acknowledgements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcknowledgementDto>()
            .ToTable("useracknowledgement", "public")
                .HasKey(a => a.Email);
        }
    }
}
