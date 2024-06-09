using BuildingBlocks.Results;
using MediatR;

namespace BuildingBlocks.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
