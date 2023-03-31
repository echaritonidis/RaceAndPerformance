using Microsoft.EntityFrameworkCore;
using RaceAndPerformance.Core.Entities;

namespace RaceAndPerformance.Core.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>()
                .HasMany(p => p.MatchOdds)
                .WithOne(c => c.Match)
                .HasForeignKey(c => c.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Match>().HasIndex(p => new { p.MatchDate, p.TeamA }).IsUnique();
            modelBuilder.Entity<Match>().HasIndex(p => new { p.MatchDate, p.TeamB }).IsUnique();
            modelBuilder.Entity<Match>().HasIndex(p => new { p.MatchDate, p.TeamA, p.TeamB }).IsUnique();
        }

        public DbSet<Match> Match { get; set; }
        public DbSet<MatchOdd> MatchOdds { get; set; }
    }
}