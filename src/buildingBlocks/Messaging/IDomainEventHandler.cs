using BuildingBlocks.Primitives;
using MediatR;

namespace BuildingBlocks.Messaging;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
where TEvent : IDomainEvent
{
}
