using Graphql_example_code.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Graphql_example_code.Presentation.Configurations;
public static class DatabaseSetup
{
    /// <summary>
    /// Extention Method to Setup Database
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        string? connString =
            configuration.GetConnectionString("GraphqlSampleConnection");

        services.AddDbContext<ProductContext>(options =>
        {
            options.UseSqlServer(connString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        });
    }
}
