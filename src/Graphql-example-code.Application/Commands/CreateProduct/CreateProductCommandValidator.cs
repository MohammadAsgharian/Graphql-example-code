using FluentValidation;
using Graphql_example_code.Application.Core;
using Graphql_example_code.Application.Core.Results;

namespace Graphql_example_code.Application.Commands.CreateProduct;
internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x._product.Title).NotNull().NotEmpty()
                .WithMessage(ErrorCodes.TITLE_CAN_NOT_EMPTY.GetDescription());
        RuleFor(x => x._product.Title).MaximumLength(500)
                .WithMessage(ErrorCodes.TITLE_MAXIMUM_LENGTH_IS_500.GetDescription());
        RuleFor(x => x._product.Title).MaximumLength(500)
                .WithMessage(ErrorCodes.DESCRIPTION_MAXIMUM_LENGTH_IS_1000.GetDescription());

    }

}
