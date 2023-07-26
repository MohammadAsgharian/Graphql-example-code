namespace Graphql_example_code.Application.Core.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<string> ErrorMessages { get; }

    public Result(
        bool isSuccess,
        List<string> errors)
    {
        IsSuccess = isSuccess;
        ErrorMessages = errors;
    }

 

    public static ResultT<T> Fail<T>(List<string> errors)
        => new ResultT<T>(default(T),false, errors);


    public static ResultT<T> Ok<T>(T value)
        => new ResultT<T>(value,true, Error.None);


}
