using Graphql_example_code.Application.Core.Results;
using Graphql_example_code.Domain;
using MediatR;

namespace Graphql_example_code.Application.Queries;
public record class GetProductQuery : IRequest<ResultT<List<Product>>>
{
    public GetProductQuery()
    {
    }
}
public class GetProductQueryHandler :
      IRequestHandler<GetProductQuery, ResultT<List<Product>>>
{
    private readonly IProduct _productRepository;
    public GetProductQueryHandler(IProduct productRepository)
        => _productRepository = productRepository;

    public async Task<ResultT<List<Product>>> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result =
                await _productRepository.GetProductAsync(cancellationToken);
            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<Product>>(Error.GetDatabaseError(ex.Message));
        }
    }
}
