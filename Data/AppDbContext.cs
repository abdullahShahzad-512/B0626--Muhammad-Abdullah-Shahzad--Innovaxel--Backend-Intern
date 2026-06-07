using Microsoft.EntityFrameworkCore;

namespace EventRegistrationAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();

            });
            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasOne(r => r.Event)
                      .WithMany()
                      .HasForeignKey(r => r.EventId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
