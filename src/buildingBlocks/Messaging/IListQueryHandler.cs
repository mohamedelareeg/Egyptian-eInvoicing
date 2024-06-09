using BuildingBlocks.Results;
using MediatR;

namespace BuildingBlocks.Messaging;
public interface IListQueryHandler<TQuery, TItem> : IRequestHandler<TQuery, Result<CustomList<TItem>>>
       where TQuery : IListQuery<TItem>
{
}
