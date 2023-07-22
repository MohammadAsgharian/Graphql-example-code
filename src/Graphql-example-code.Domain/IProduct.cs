namespace Graphql_example_code.Domain;
public interface IProduct
{
    public Task<List<Product>> GetProductAsync(CancellationToken cancellationToken);
    public Task<bool> AddProductAsync(Product product, CancellationToken cancellationToken);
    public Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken);
    public Task<bool> DeleteProductAsync(Guid id, CancellationToken cancellationToken);
}
