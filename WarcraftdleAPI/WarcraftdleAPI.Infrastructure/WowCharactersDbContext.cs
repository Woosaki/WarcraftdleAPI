using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using WarcraftdleAPI.Domain.WowCharacter;

namespace WarcraftdleAPI.Infrastructure;

public class WowCharactersDbContext(DbContextOptions<WowCharactersDbContext> options) : DbContext(options)
{
	public DbSet<WowCharacter> WowCharacter { get; set; }
	public DbSet<Race> Race { get; set; }
	public DbSet<Expansion> Expansion { get; set; }
	public DbSet<Class> Class { get; set; }
	public DbSet<Gender> Gender { get; set; }
	public DbSet<Zone> Zone { get; set; }
	public DbSet<Affiliation> Affiliation { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
