using FluentValidation.Results;
using Graphql_example_code.Application.Core.Commands;
using Graphql_example_code.Application.Core.Results;

namespace Graphql_example_code.Application.Commands.CreateProduct;
public record CreateProductCommand : Command<ResultT<Guid>>
{
    public CreateProductRequest _product { get; init; }

    public CreateProductCommand(
        CreateProductRequest product
        )
        => _product = product;


    public override ValidationResult Validate()
    {
        return new CreateProductCommandValidator().Validate(this);
    }
}
