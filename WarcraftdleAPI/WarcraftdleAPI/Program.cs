using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Application.Dtos.Affiliation;
using WarcraftdleAPI.Application.Dtos.WowCharacter;
using WarcraftdleAPI.Application.Dtos.Zone;
using WarcraftdleAPI.Application.Services;
using WarcraftdleAPI.Application.Validators;
using WarcraftdleAPI.Infrastructure;
using WarcraftdleAPI.Middlewares;

namespace WarcraftdleAPI;

public static class Program
{
	private static void ConfigureDatabase(this WebApplicationBuilder builder)
	{
		var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
		builder.Services.AddDbContext<WarcraftdleDbContext>(
			options => options.UseNpgsql(connectionString));
	}

	private static void ConfigureServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<AffiliationService>();
		builder.Services.AddScoped<ZoneService>();
		builder.Services.AddScoped<WowCharacterService>();

		builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
		builder.Services.AddTransient<IValidator<AddWowCharacterRequest>, AddWowCharacterRequestValidator>();
		builder.Services.AddTransient<IValidator<AddZoneRequest>, AddZoneRequestValidator>();
		builder.Services.AddTransient<IValidator<AddAffiliationRequest>, AddAffiliationRequestValidator>();
		builder.Services.AddTransient<IValidator<AddMultipleAffiliationRequest>, AddMultipleAffiliationRequestValidator>();
		builder.Services.AddTransient<IValidator<AddMultipleZoneRequest>, AddMultipleZoneRequestValidator>();

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
	}

	private static void ConfigureSwagger(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}
	}

	private static void ConfigureMiddleware(this WebApplication app)
	{
		app.UseMiddleware<ExceptionHandlerMiddleware>();
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
		builder.ConfigureServices();

		var app = builder.Build();

		app.ConfigureSwagger();
		app.ConfigureMiddleware();

		app.UseAuthorization();
		app.MapControllers();

		app.MigrateDatabase();

		app.Run();
	}
}
