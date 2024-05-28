using Microsoft.EntityFrameworkCore;
using Serilog;
using WarcraftdleAPI.Application.Extensions;
using WarcraftdleAPI.Extensions;
using WarcraftdleAPI.Infrastructure;
using WarcraftdleAPI.Infrastructure.Extensions;
using WarcraftdleAPI.Middlewares;

namespace WarcraftdleAPI;

public static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddPresentation();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<WarcraftdleDbContext>();

                if (db.Database.CanConnect())
                {
                    if (db.Database.GetPendingMigrations().Any())
                    {
                        db.Database.Migrate();
                    }
                }
                else throw new Exception("Can't connect to the database");
            }

            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }

        catch (Exception ex)
        {
            Log.Fatal(ex, "Application startup failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}