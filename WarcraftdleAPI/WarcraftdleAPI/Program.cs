using WarcraftdleAPI.Application.Extensions;
using WarcraftdleAPI.Extensions;
using WarcraftdleAPI.Infrastructure.Extensions;

namespace WarcraftdleAPI;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.ConfigureServices();
        builder.ConfigureCors();

        var app = builder.Build();

        app.ConfigureSwagger();
        app.ConfigureMiddleware();
        app.UseCors("CorsPolicy");
        app.UseAuthorization();
        app.MapControllers();

        app.MigrateDatabase();

        app.Run();
    }
}