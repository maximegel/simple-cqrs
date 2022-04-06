using SimpleCqrs.Shared.Domain.Events;

namespace SimpleCqrs.Shared.Domain.Snapshots;

public interface ISnapshot :
    IDomainEvent
{
}