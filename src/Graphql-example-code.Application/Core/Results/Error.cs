namespace Graphql_example_code.Application.Core.Results;
public class Error
{
    public string Message { get; }
    public Error(string message)
    {
        Message = message;
    }

    /// <summary>
    /// We don't have an Error
    /// </summary>
    public static readonly List<string> None = new List<string>();
    public static readonly List<string> NotFound = new List<string> { new("Not Found Error")};

    /// <summary>
    /// Error to Retrieve data from database
    /// </summary>
    public static List<string> GetDatabaseError(string message)
        => new List<string>() { new(message) };

}
