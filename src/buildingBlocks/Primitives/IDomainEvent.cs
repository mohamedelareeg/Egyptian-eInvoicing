using MediatR;

namespace BuildingBlocks.Primitives;

// Outbox Pattern
// Domain Event Pattern
public interface IDomainEvent : INotification
{
    Guid Id { get; }
}
