namespace BuildingBlocks.Primitives;

public abstract record DomainEvent(Guid Id) : IDomainEvent;
