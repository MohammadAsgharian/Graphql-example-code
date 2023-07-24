using MediatR;
using Graphql_example_code.Application.Core.Results;
using FluentValidation.Results;

namespace Graphql_example_code.Application.Core.Commands;
public interface ICommand<out TResult> : IRequest<TResult>
{
    public abstract ValidationResult Validate();
}
public abstract record class Command<T> : ICommand<CommandHandlerResult<T>> where T : Result
{
    public ValidationResult ValidationResult { get; init; }
    public virtual ValidationResult Validate()
    {
        return ValidationResult;
    }
}
