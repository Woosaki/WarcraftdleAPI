using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Domain.User;

namespace WarcraftdleAPI.Infrastructure;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
	public DbSet<User> User { get; set; }
	public DbSet<Role> Role { get; set; }
}
