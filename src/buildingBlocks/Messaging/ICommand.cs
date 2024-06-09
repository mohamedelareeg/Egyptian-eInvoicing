using BuildingBlocks.Results;
using MediatR;

namespace BuildingBlocks.Messaging;

public interface ICommand : IRequest<Result>
{

}
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{

}
