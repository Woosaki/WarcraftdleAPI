using Serilog;
using WarcraftdleAPI.Middlewares;

namespace WarcraftdleAPI.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        builder.ConfigureCors();
    }

    private static void ConfigureCors(this WebApplicationBuilder builder)
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
