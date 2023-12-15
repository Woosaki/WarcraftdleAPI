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

	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.ConfigureDatabase();
		builder.Services.AddScoped<IWowCharacterService, WowCharacterService>();
		builder.Services.AddControllers();
		builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
		builder.Services.AddScoped<IValidator<CharacterAddRequest>, CharacterAddRequestValidator>();
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
