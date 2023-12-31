using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Application.Services;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI;

public static class Program
{
	private static void ConfigureDatabase(this WebApplicationBuilder builder)
	{
		var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
		builder.Services.AddDbContext<WarcraftdleDbContext>(
			options => options.UseNpgsql(connectionString));
	}

	private static void MigrateDatabase(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var db = scope.ServiceProvider.GetRequiredService<WarcraftdleDbContext>();

		if (db.Database.GetPendingMigrations().Any())
		{
			db.Database.Migrate();
		}
	}

	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.ConfigureDatabase();

		builder.Services.AddScoped<AffiliationService>();

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}
		app.UseHttpsRedirection();
		app.UseAuthorization();
		app.MapControllers();

		app.MigrateDatabase();

		app.Run();
	}
}
