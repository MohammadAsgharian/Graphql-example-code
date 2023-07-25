namespace Graphql_example_code.Application.Commands.UpdateProduct;
public record UpdateProductRequest(
    Guid Id,
    string Title,
    string Description);
