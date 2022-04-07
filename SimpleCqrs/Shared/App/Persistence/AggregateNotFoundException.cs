namespace SimpleCqrs.Shared.App.Persistence;

public class AggregateNotFoundException : Exception
{
    public AggregateNotFoundException() :
        base("Aggregate not found.")
    {
    }
}