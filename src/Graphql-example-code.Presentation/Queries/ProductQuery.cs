using MediatR;
using Graphql_example_code.Domain;
using Graphql_example_code.Application.Core.Results;
using Graphql_example_code.Application.Queries.GetProduct;



namespace Graphql_example_code.Presentation.Queries;
public class ProductQuery
{
    public async Task<ResultT<List<Product>>> GetProductListAsync([Service] IMediator mediator)
    {
        return await mediator.Send(new GetProductQuery());
    }
}
