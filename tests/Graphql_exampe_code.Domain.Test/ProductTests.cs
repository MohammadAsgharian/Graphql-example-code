using Graphql_example_code.Domain;
using Xunit;

namespace Domain.Test;
public class ProductTests
{
    [Fact]
    public void Product_CreateNew_ShouldSetProperties()
    {
        // Arrange
        string title = "Test Product";
        string description = "This is a test product.";

        // Act
        var product = Product.CreateNew(title, description);

        // Assert 
        Assert.Equal(title, product.Title);
        Assert.Equal(description, product.Description);
    }

  

    [Fact]
    public void Product_Equals_ShouldReturnFalseForDifferentProducts()
    {
        // Arrange
        var product1 = Product.CreateNew("Test Product 1", "Description 1");
        var product2 = Product.CreateNew("Test Product 2", "Description 2");

        // Assert
        Assert.NotEqual(product1, product2);
    }
}