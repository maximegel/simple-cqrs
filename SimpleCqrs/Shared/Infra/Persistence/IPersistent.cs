using SimpleCqrs.Shared.Domain;

namespace SimpleCqrs.Shared.Infra.Persistence;

public interface IPersistent : IAggregateRoot
{
    IIdentifier Identifier { get; set; }
}