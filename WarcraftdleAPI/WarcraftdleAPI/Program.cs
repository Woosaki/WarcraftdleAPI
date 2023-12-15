using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Application.Dto;
using WarcraftdleAPI.Application.Interfaces;
using WarcraftdleAPI.Application.Services;
using WarcraftdleAPI.Application.Validators;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI;

public static class Program
{
	private static void ConfigureDatabase(this WebApplicationBuilder builder)
	{
		var userConnectionString = builder.Configuration.GetConnectionString("UserConnectionString");
		builder.Services.AddDbContext<UsersDbContext>(
			options => options.UseSqlServer(userConnectionString));

		var characterConnectionString = builder.Configuration.GetConnectionString("CharacterConnectionString");
		builder.Services.AddDbContext<WowCharactersDbContext>(
			options => options.UseSqlServer(characterConnectionString));
	}

	private static void ConfigureServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddControllers();

		builder.Services.AddScoped<IWowCharacterService, WowCharacterService>();

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
	}

	private static void ConfigureFluentValidation(this WebApplicationBuilder builder)
	{
		builder.Services.AddFluentValidationAutoValidation();
		builder.Services.AddFluentValidationClientsideAdapters();

		builder.Services.AddScoped<IValidator<CharacterAddRequest>, CharacterAddRequestValidator>();
	}

	private static void ConfigureMiddlewarePipeline(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}
		app.UseHsts();
		app.UseHttpsRedirection();
		app.UseRouting();
		app.UseAuthorization();
	}

	private static void ConfigureRoutes(this WebApplication app)
	{
		app.MapControllers();
	}

	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.ConfigureDatabase();
		builder.ConfigureServices();
		builder.ConfigureFluentValidation();	

		var app = builder.Build();
		app.ConfigureMiddlewarePipeline();
		app.ConfigureRoutes();

		app.Run();
	}
}
