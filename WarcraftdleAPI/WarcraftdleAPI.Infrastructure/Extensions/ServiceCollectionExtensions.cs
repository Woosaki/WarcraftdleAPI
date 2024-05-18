using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarcraftdleAPI.Domain.Repositories;
using WarcraftdleAPI.Infrastructure.Repositories;

namespace WarcraftdleAPI.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnectionString");
        services.AddDbContext<WarcraftdleDbContext>(options =>
        {
            options.UseNpgsql(connectionString)
                .EnableSensitiveDataLogging();
        });

        services.AddScoped<IZonesRepository, ZonesRepository>();
        services.AddScoped<IAffiliationsRepository, AffiliationsRepository>();
    }
}
