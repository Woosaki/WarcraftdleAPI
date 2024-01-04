using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI;

public static class Program
{
	private static void ConfigureDatabase(this WebApplicationBuilder builder)
	{

		var connectionString = builder.Configuration.GetConnectionString("CharacterConnectionString");
		builder.Services.AddDbContext<WarcraftdleDbContext>(
			options => options.UseSqlServer(connectionString));
	}

	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.ConfigureDatabase();

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

		app.Run();
	}
}
