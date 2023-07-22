namespace Graphql_example_code.Domain;
public class Product
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

    public static Product CreateNew(
      string title,
      string description)
    {
        return new Product(
            title,
            description);
    }
    private Product(
        string title,
        string description)
    {
       Id = Guid.NewGuid();
        Title = title;
        Description = description;
    }

    // constructor for EF
    private Product() { }
}
