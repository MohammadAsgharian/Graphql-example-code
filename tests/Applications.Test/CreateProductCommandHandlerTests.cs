using Moq;
using Xunit;
using Graphql_example_code.Domain;
using Graphql_example_code.Application.Core.Results;
using Graphql_example_code.Application.Core.Commands;
using Graphql_example_code.Application.Commands.CreateProduct;
using Microsoft.Extensions.DependencyInjection;
using Graphql_example_code.Infrastructure.Repositories;

namespace Applications.Test;
public class CreateProductCommandHandlerTests
{
    [Fact]
    public async Task ExecuteCommand_ValidRequest_ReturnsSuccessResult()
    {
        // Arrange
        var productRepositoryMock = new Mock<IProduct>();
        var handler = new CreateProductCommandHandler(productRepositoryMock.Object);
        var createProductRequest = new CreateProductRequest("Test Product", "Test Description");
        var command = new CreateProductCommand(createProductRequest);

        var cancellationToken = new CancellationToken();

        productRepositoryMock
            .Setup(repo => repo.AddProductAsync(It.IsAny<Product>(), cancellationToken))
            .ReturnsAsync(Guid.NewGuid());

        // Act
        var result = await handler.ExecuteCommand(
            new CommandHandlerResult<ResultT<Guid>>(command),
            command,
            cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ExecuteCommand_InvalidRequest_Tilte_CAN_NOT_EMPTY_ReturnsErrorResult()
    {
        // Arrange
        var productRepositoryMock = new Mock<IProduct>();
        var handler = new CreateProductCommandHandler(productRepositoryMock.Object);
        var createProductRequest = new CreateProductRequest("", "Test Description");
        var command = new CreateProductCommand(createProductRequest);


        var cancellationToken = new CancellationToken();


        // Act
        var result = await handler.ExecuteCommand(
            new CommandHandlerResult<ResultT<Guid>>(command),
            command,
            cancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Guid.Empty, result.Value);
        Assert.NotEmpty(result.ErrorMessages);
        Assert.Equal(result.ErrorMessages[0], "Title Can not Empty");
    }

    [Fact]
    public async Task ExecuteCommand_InvalidRequest_Title_Description_Maximum_Length_is_Checked_ReturnsErrorResult()
    {
        // Arrange
        var productRepositoryMock = new Mock<IProduct>();
        var handler = new CreateProductCommandHandler(productRepositoryMock.Object);
        string Title_Length_501 = Core.RandomString(501); // Create Title with 501 char
        string Description_Length_1001 = Core.RandomString(1001); // Create Description with 1001 char
        var createProductRequest = 
            new CreateProductRequest(Title_Length_501, Description_Length_1001);

        var command = new CreateProductCommand(createProductRequest);


        var cancellationToken = new CancellationToken();


        // Act
        var result = await handler.ExecuteCommand(
            new CommandHandlerResult<ResultT<Guid>>(command),
            command,
            cancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Guid.Empty, result.Value);
        Assert.NotEmpty(result.ErrorMessages);
        Assert.Equal(result.ErrorMessages[0], "Title Maximum Length is 500");
        Assert.Equal(result.ErrorMessages[1], "Description Maximum Length is 1000");
    }
}
