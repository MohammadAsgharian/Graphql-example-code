using Graphql_example_code.Application.Queries.GetProduct;
using Graphql_example_code.Domain;
using Graphql_example_code.Infrastructure.Persistence;
using Graphql_example_code.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Graphql_example_code.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(
        this IServiceCollection services)
    {
        services.AddMediatR(typeof(GetProductQuery).GetTypeInfo().Assembly);
        services.AddScoped<IProduct, ProductRepository>();

       

        return services;
    }

    public static IServiceProvider ProviderServices(
       this IServiceProvider services)
    {

        using (var scope = services.CreateScope())
        {
            var serviceProviders = scope.ServiceProvider;
            var context = serviceProviders.GetRequiredService<ProductContext>();
            SeedData.Initialize(serviceProviders);
        }


        return services;
    }
}