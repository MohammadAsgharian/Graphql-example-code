using MediatR;
using Graphql_example_code.Application.Core.Results;


namespace Graphql_example_code.Application.Core.Commands;
public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
  where TCommand : ICommand<TResult>
{
}

public abstract class CommandHandler<TCommand, TID> : ICommandHandler<TCommand, CommandHandlerResult<TID>>
    where TCommand : ICommand<CommandHandlerResult<TID>>
    where TID : Result
{
    public abstract Task<TID> ExecuteCommand(CommandHandlerResult<TID> result, TCommand command, CancellationToken cancellationToken = default);

    public async Task<CommandHandlerResult<TID>> Handle(TCommand command, CancellationToken cancellationToken = default)
    {
        CommandHandlerResult<TID> result = new CommandHandlerResult<TID>(command);

        try
        {
            result.Id = await ExecuteCommand(result, command, cancellationToken);
        }
        catch (Exception ex) { throw; }
        return result;
    }
}
