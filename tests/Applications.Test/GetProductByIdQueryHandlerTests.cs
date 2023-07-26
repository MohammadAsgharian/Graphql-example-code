using Graphql_example_code.Application.Queries;
using Graphql_example_code.Domain;
using Moq;
using Xunit;

namespace Applications.Test;
public class GetProductByIdQueryHandlerTests
{
    [Fact]
    public async Task GetProductByIdQueryHandler_Should_Return_ResultT_With_Product()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        // Create a mock of the IProduct repository
        var productRepositoryMock = new Mock<IProduct>();
        // Set up the expected behavior of the GetProductAsync method
        var expectedProduct =
            Product.CreateNew("Product 1", "Description 1");
        
        productRepositoryMock
            .Setup(repo => repo.GetProductByIdAsync(expectedProduct.Id, cancellationToken))
            .ReturnsAsync(expectedProduct);

        // Create an instance of the GetProductQueryHandler, injecting the mocked IProduct repository
        var handler = new GetProductByIdQueryHandler(productRepositoryMock.Object);

        // Create an instance of the GetProductQuery
        var query = new GetProductByIdQuery(expectedProduct.Id);

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess); // Check if the ResultT indicates success
        Assert.False(result.IsFailure); // Check if the ResultT indicates success
        Assert.Empty(result.ErrorMessages); // Check if there's no error

        // Validate the returned products
        Assert.Equal(expectedProduct.Id, result.Value.Id);
        Assert.Equal(expectedProduct.Title, result.Value.Title);
        Assert.Equal(expectedProduct.Description, result.Value.Description);
    }
    [Fact]
    public async Task GetProductByIdQueryHandler_Should_Return_ResultT_With_Error_On_Exception()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        // Create a mock of the IProduct repository
        var productRepositoryMock = new Mock<IProduct>();
        Guid Id = Guid.Parse("e0b869b6-a394-4de0-9eed-8035527b2469");

        // Set up the expected behavior of the GetProductAsync method to throw an exception
        productRepositoryMock
            .Setup(repo => repo.GetProductByIdAsync(Id, cancellationToken))
            .ThrowsAsync(new Exception("Test exception"));

        // Create an instance of the GetProductQueryHandler, injecting the mocked IProduct repository
        var handler = new GetProductByIdQueryHandler(productRepositoryMock.Object);

        // Create an instance of the GetProductQuery
        var query = new GetProductByIdQuery(Id);

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.False(result.IsSuccess); // Check if the ResultT indicates success (even on exception)
        Assert.True(result.IsFailure);  // Check if the ResultT indicates failure (even on exception)
        Assert.Single(result.ErrorMessages); // Check if there's an error list
        Assert.Null(result.Value); // Check result.Value is Null
        // You can also check the specific error message if you have them defined.
        Assert.Equal("Test exception", result.ErrorMessages[0]);
    }
}
