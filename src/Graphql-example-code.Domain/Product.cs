namespace Graphql_example_code.Domain;
public class Product : Entity<Guid>
{
    public string Title { get; private set; }
    public string Description { get; private set; }

    private Product(
      string title,
      string description)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
    }

    public static Product CreateNew(
      string title,
      string description)
    {
        return new Product(
            title,
            description);
    }
  
    public void SetUpdate(
          string title,
          string description)
    {
        Title = title;
        Description = description;
    }

    // constructor for EF
    private Product() { }
}
