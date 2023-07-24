using FluentValidation.Results;
using Graphql_example_code.Application.Core.Results;


namespace Graphql_example_code.Application.Core.Commands;
public interface ICommandHandlerResult
{
    public ValidationResult ValidationResult { get; set; }
}

public sealed record class CommandHandlerResult<TID> : ICommandHandlerResult where TID : Result
{
    public TID Id { get; set; }
    public ValidationResult ValidationResult { get; set; }

    public CommandHandlerResult(ICommand<ICommandHandlerResult> command)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));

        ValidationResult = command.Validate();
    }
}
