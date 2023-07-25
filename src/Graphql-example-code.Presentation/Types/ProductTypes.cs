using MediatR;
using Graphql_example_code.Domain;
using Graphql_example_code.Application.Queries;
using Graphql_example_code.Application.Core.Results;


namespace Graphql_example_code.Presentation.Types;
public class ProductTypes
{
    public async Task<ResultT<List<Product>>> GetProductListAsync([Service] IMediator mediator)
    {
        return await mediator.Send(new GetProductQuery());
    }
}
