namespace Graphql_example_code.Domain;
public interface IProduct
{
    public Task<List<Product>> GetProductAsync(CancellationToken cancellationToken);
    public Task<Product> GetProductByIdAsync(Guid Id,CancellationToken cancellationToken);
    public Task<Guid> AddProductAsync(Product product, CancellationToken cancellationToken);
    public Task<Guid> UpdateProductAsync(Product product, CancellationToken cancellationToken);
    public Task<bool> DeleteProductAsync(Guid id, CancellationToken cancellationToken);
}
