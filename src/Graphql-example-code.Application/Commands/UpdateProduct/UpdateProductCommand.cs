using FluentValidation.Results;
using Graphql_example_code.Application.Commands.CreateProduct;
using Graphql_example_code.Application.Core.Commands;
using Graphql_example_code.Application.Core.Results;

namespace Graphql_example_code.Application.Commands.UpdateProduct
{
    public record class UpdateProductCommand : Command<ResultT<Guid>>
    {
        public UpdateProductRequest _product { get; init; }

        public UpdateProductCommand(
            UpdateProductRequest product
            )
            => _product = product;


        public override ValidationResult Validate()
        {
            return new UpdateProductCommandValidator().Validate(this);
        }
    }

}
