namespace Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{

}

public interface IQuery<out TResponse> : IRequest<TResponse>
{

}
