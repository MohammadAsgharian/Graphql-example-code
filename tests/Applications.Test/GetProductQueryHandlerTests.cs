using Moq;
using Xunit;
using Graphql_example_code.Domain;
using Graphql_example_code.Application.Queries;


namespace Applications.Test;
public class GetProductQueryHandlerTests
{
    [Fact]
    public async Task GetProductQueryHandler_Should_Return_ResultT_With_Products()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        // Create a mock of the IProduct repository
        var productRepositoryMock = new Mock<IProduct>();
        // Set up the expected behavior of the GetProductAsync method
        var expectedProducts = new List<Product>
        {
            Product.CreateNew("Product 1","Description 1" ),
            Product.CreateNew("Product 2","Description 2" )
        };
        productRepositoryMock
            .Setup(repo => repo.GetProductAsync(cancellationToken))
            .ReturnsAsync(expectedProducts);

        // Create an instance of the GetProductQueryHandler, injecting the mocked IProduct repository
        var handler = new GetProductQueryHandler(productRepositoryMock.Object);

        // Create an instance of the GetProductQuery
        var query = new GetProductQuery();

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess); // Check if the ResultT indicates success
        Assert.False(result.IsFailure); // Check if the ResultT indicates success
        Assert.Empty(result.ErrorMessages); // Check if there's no error

        // Validate the returned products
        Assert.Equal(expectedProducts[0].Id, result.Value[0].Id);
        Assert.Equal(expectedProducts[0].Title, result.Value[0].Title);
        Assert.Equal(expectedProducts[0].Description, result.Value[0].Description);
        Assert.Equal(expectedProducts[1].Id, result.Value[1].Id);
        Assert.Equal(expectedProducts[1].Title, result.Value[1].Title);
        Assert.Equal(expectedProducts[1].Description, result.Value[1].Description);
    }
    [Fact]
    public async Task GetProductQueryHandler_Should_Return_ResultT_With_Error_On_Exception()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        // Create a mock of the IProduct repository
        var productRepositoryMock = new Mock<IProduct>();
        // Set up the expected behavior of the GetProductAsync method to throw an exception
        productRepositoryMock
            .Setup(repo => repo.GetProductAsync(cancellationToken))
            .ThrowsAsync(new Exception("Test exception"));

        // Create an instance of the GetProductQueryHandler, injecting the mocked IProduct repository
        var handler = new GetProductQueryHandler(productRepositoryMock.Object);

        // Create an instance of the GetProductQuery
        var query = new GetProductQuery();

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