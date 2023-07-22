using Graphql_example_code.Application.Queries.GetProduct;
using Graphql_example_code.Domain;
using MediatR;

namespace Graphql_example_code.Presentation.Queries
{
    public class ProductQuery
    {
        public async Task<List<Product>> GetProductListAsync([Service] IMediator mediator)
        {
            return await mediator.Send(new GetProductQuery());
        }
    }
}
