using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WarcraftdleAPI.Domain.User;

namespace WarcraftdleAPI.Infrastructure;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
	public DbSet<User> User { get; set; }
	public DbSet<Role> Role { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
