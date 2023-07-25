using Graphql_example_code.Application.Core.Results;
using Graphql_example_code.Domain;
using MediatR;

namespace Graphql_example_code.Application.Queries;
public record class GetProductByIdQuery 
    : IRequest<ResultT<Product>>
{
    public Guid _id { get; init; }
    public GetProductByIdQuery(Guid id)
    {
        _id = id;
    }
}
public class GetProductQueryByIdHandler :
      IRequestHandler<GetProductByIdQuery, ResultT<Product>>
{
    private readonly IProduct _productRepository;
    public GetProductQueryByIdHandler(IProduct productRepository)
        => _productRepository = productRepository;

    public async Task<ResultT<Product>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result =
                await _productRepository.GetProductByIdAsync(query._id,cancellationToken);
            return new ResultT<Product>(
                result,
                true,
                Error.None);
        }
        catch (Exception ex)
        {
            return new ResultT<Product>(
                null,
                false,
                Error.GetDatabaseError(ex.Message));
        }
    }
}
