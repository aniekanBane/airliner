namespace Application.Abstractions.Messaging;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{

}

public interface ICommand<out TResponse> : IRequest<TResponse>
{

}
