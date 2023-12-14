using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Domain.User;

namespace WarcraftdleAPI.Infrastructure;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
	public DbSet<User> User { get; set; }
	public DbSet<Role> Role { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Role>().HasData(
			new Role { Id = 1, Name = "User" },
			new Role { Id = 2, Name = "Admin" });
	}
}
