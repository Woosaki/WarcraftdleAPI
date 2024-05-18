using WarcraftdleAPI.Application.Services;

namespace WarcraftdleAPI.Extensions;

public static class BuilderExtensions
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
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
                corsBuilder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
        });
    }
}