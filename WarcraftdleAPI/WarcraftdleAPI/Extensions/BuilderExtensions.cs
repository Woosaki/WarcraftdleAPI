using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Application.Dtos.Affiliations;
using WarcraftdleAPI.Application.Dtos.WowCharacters;
using WarcraftdleAPI.Application.Dtos.Zones;
using WarcraftdleAPI.Application.Services;
using WarcraftdleAPI.Application.Validators.Affiliations;
using WarcraftdleAPI.Application.Validators.WowCharacters;
using WarcraftdleAPI.Application.Validators.Zones;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI.Extensions;

public static class BuilderExtensions
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
        builder.Services.AddDbContext<WarcraftdleDbContext>(
            options => options.UseNpgsql(connectionString));
    }

    public static void ConfigureFluentValidation(this WebApplicationBuilder builder)
    {
        builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        builder.Services.AddTransient<IValidator<AddWowCharacterRequest>, AddWowCharacterRequestValidator>();
        builder.Services.AddTransient<IValidator<AddZoneRequest>, AddZoneRequestValidator>();
        builder.Services.AddTransient<IValidator<AddAffiliationRequest>, AddAffiliationRequestValidator>();
        builder.Services.AddTransient<IValidator<AddMultipleAffiliationRequest>, AddMultipleAffiliationRequestValidator>();
        builder.Services.AddTransient<IValidator<AddMultipleZoneRequest>, AddMultipleZoneRequestValidator>();
    }

    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        builder.Services.AddScoped<AffiliationService>();
        builder.Services.AddScoped<ZoneService>();
        builder.Services.AddScoped<WowCharacterService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    public static void ConfigureCors(this WebApplicationBuilder builder)
    {
        var developmentOrigin = builder.Configuration["CORS:DevelopmentOrigin"];
        var productionOrigin = builder.Configuration["CORS:ProductionOrigin"];

        if (string.IsNullOrWhiteSpace(developmentOrigin) || string.IsNullOrWhiteSpace(productionOrigin))
        {
            throw new ArgumentException("The CORS setting must be set in the configuration properly.");
        }

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", corsBuilder =>
            {
                corsBuilder.WithOrigins(developmentOrigin, productionOrigin)
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
        });
    }
}