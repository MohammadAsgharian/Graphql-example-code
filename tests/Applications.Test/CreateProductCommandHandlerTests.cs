using Moq;
using Xunit;
using Graphql_example_code.Domain;
using Graphql_example_code.Application.Core.Results;
using Graphql_example_code.Application.Core.Commands;
using Graphql_example_code.Application.Commands.CreateProduct;
using Microsoft.Extensions.DependencyInjection;
using Graphql_example_code.Infrastructure.Repositories;
using Graphql_example_code.Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading;

namespace Applications.Test;
public class CreateProductCommandHandlerTests
{

    [Fact]
    public async Task CreateProductCommandHandler_Should_AddProductToDatabase()
    {
        // Arrange
        var createProductRequest = 
            new CreateProductRequest("Test Product", "This is a test product.");
        string ConnectionString = 
            "data source=.;initial catalog=GraphqlSample;TrustServerCertificate=True;Integrated Security=true";
        var options = new DbContextOptionsBuilder<ProductContext>()
                .UseSqlServer(ConnectionString)
                .Options;


        var createProductCommand = new CreateProductCommand(createProductRequest);
        var productRepository = new ProductRepository(new ProductContext(options));
        var commandHandler = new CreateProductCommandHandler(productRepository);

        var cancellationToken = 
            new CancellationToken();

        // Act
        var validationResult = createProductCommand.Validate();
        var result = await commandHandler.ExecuteCommand(
            new CommandHandlerResult<ResultT<Guid>>(createProductCommand),
            createProductCommand,
            cancellationToken);

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value); // Check if a new GUID was returned
        Assert.NotNull(await productRepository.GetProductByIdAsync(result.Value, default)); // Check if the product was added to the database
    }


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
