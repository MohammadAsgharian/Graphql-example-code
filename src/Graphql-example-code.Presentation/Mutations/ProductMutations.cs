using Graphql_example_code.Application.Commands.CreateProduct;
using Graphql_example_code.Application.Commands.UpdateProduct;
using Graphql_example_code.Application.Core.Results;
using MediatR;

namespace Graphql_example_code.Presentation.Mutations
{
    public class ProductMutations
    {
        public async Task<ResultT<Guid>> AddProductAsync(
            [Service] IMediator mediator,
            CreateProductRequest createProductRequest)
        {
            var result =
                await mediator.Send(new CreateProductCommand(createProductRequest));
            return result.Id;
        }

        public async Task<ResultT<Guid>> UpdateProductAsync(
           [Service] IMediator mediator,
           UpdateProductRequest updateProductRequest)
        {
            var result =
                await mediator.Send(new UpdateProductCommand(updateProductRequest));
            return result.Id;
        }
    }
}
