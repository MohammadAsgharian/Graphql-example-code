using Graphql_example_code.Domain;
using MediatR;

namespace Graphql_example_code.Application.Queries.GetProduct;

public record class GetProductQuery : IRequest<List<Product>>
{
    public GetProductQuery()
    {
    }
}
public class GetProductQueryHandler :
      IRequestHandler<GetProductQuery, List<Product>>
{
    private readonly IProduct _productRepository;
    public GetProductQueryHandler(IProduct productRepository)
        => this._productRepository = productRepository;

    public async Task<List<Product>> Handle(GetProductQuery query, CancellationToken cancellationToken)
        => await _productRepository.GetProductAsync(cancellationToken);
}
