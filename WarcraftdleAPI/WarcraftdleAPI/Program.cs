using WarcraftdleAPI.Extensions;

namespace WarcraftdleAPI;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.ConfigureDatabase();
        builder.ConfigureFluentValidation();
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