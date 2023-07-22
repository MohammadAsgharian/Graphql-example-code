using Graphql_example_code.Domain;
using Graphql_example_code.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Graphql_example_code.Infrastructure.Repositories;

public class ProductRepository : IProduct
{
    private readonly ProductContext _db;
    public ProductRepository(ProductContext db)
    {
        _db = db;
    }
    public async Task<bool> AddProductAsync(Product product, CancellationToken cancellationToken)
    {
        await _db.Products.AddAsync(product, cancellationToken);
        return await _db.SaveChangesAsync(cancellationToken) > 0;
    }
       

    public async Task<bool> DeleteProductAsync(long id, CancellationToken cancellationToken)
        => await _db.Products.Where(product => product.Id == id).ExecuteDeleteAsync(cancellationToken) > 0;


    public async Task<List<Product>> GetProductAsync(CancellationToken cancellationToken)
        => await _db.Products.ToListAsync();
       

    public async Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken)
    => await _db.Products.Where(product => product.Id == product.Id).ExecuteUpdateAsync(s => s.SetProperty(b => b.Title, product.Title)
                                                                                                .SetProperty(b => b.Description, product.Description),cancellationToken) > 0;

}
