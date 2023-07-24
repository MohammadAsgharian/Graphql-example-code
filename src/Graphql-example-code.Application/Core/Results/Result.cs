namespace Graphql_example_code.Application.Core.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<string> Error { get; }

    public Result(
        bool isSuccess,
        List<string> error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
}
