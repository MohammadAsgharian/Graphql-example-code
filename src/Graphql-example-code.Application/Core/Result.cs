

namespace Graphql_example_code.Application.Core;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Message { get; }
    public List<string> Error { get; }

    public Result(
        bool isSuccess,
        string message,
        List<string> error)
    {
        IsSuccess = isSuccess;
        Error = error;
        Message = message;
    }
}
