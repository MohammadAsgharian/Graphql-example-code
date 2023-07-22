using Graphql_example_code.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Graphql_example_code.Infrastructure.Persistence;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ProductContext(
            serviceProvider.GetRequiredService<DbContextOptions<ProductContext>>()))
        {
            if (context.Products.Any())
                return;
            
            context.Products.AddRange(
                Product.CreateNew("PS5","Sony game console"),
                Product.CreateNew("XBox", "Microsoft game console"));
            context.SaveChanges();
        }
    }
}
