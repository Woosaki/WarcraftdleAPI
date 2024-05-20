using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Domain.Entities;

namespace WarcraftdleAPI.Infrastructure;

public class WarcraftdleDbContext(DbContextOptions<WarcraftdleDbContext> options) : DbContext(options)
{
    public DbSet<Character> Character { get; set; }
    public DbSet<Zone> Zone { get; set; }
    public DbSet<Affiliation> Affiliation { get; set; }
}