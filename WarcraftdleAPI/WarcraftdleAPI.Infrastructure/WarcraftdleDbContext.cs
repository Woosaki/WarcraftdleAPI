using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Domain.Entities;
using WarcraftdleAPI.Domain.Enums;

namespace WarcraftdleAPI.Infrastructure;

public class WarcraftdleDbContext(DbContextOptions<WarcraftdleDbContext> options) : DbContext(options)
{
    public DbSet<Character> Character { get; set; }
    public DbSet<Zone> Zone { get; set; }
    public DbSet<Affiliation> Affiliation { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Character>()
            .Property(e => e.Expansions)
            .HasConversion(
                v => v.ToArray(),
                v => new List<Expansion>(v));
    }

}