using Graphql_example_code.Domain;
using Microsoft.EntityFrameworkCore;

namespace Graphql_example_code.Infrastructure.Persistence;

public class ProductContext :DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
}
