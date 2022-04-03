using SimpleCqrs.Shared.Domain;

namespace SimpleCqrs.Shared.Infra.Persistence;

public abstract class Persistent<TSelf> : 
    IPersistent
    where TSelf : Persistent<TSelf>, new()
{
    IIdentifier IPersistent.Identifier
    {
        get => Identifier; 
        set => Identifier = value;
    }

    IIdentifier IEntity.Id => Identifier;
    
    protected abstract IIdentifier Identifier { get; set; }
}