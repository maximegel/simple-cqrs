namespace SimpleCqrs.Shared.Domain.ValueObjects.Identifiers;

public abstract class Uuid<TSelf> : Identifier<TSelf, Guid>
    where TSelf : Uuid<TSelf>, new()
{
    protected Uuid() : base(Guid.Empty)
    {
    }

    public static TSelf Default { get; } = new();

    public static implicit operator Uuid<TSelf>(string value) =>
        Parse(value);

    public static TSelf Generate() =>
        new() { Value = Guid.NewGuid() };

    public static TSelf Parse(Guid input) =>
        Parse(input.ToString());
    
    public static TSelf Parse(string input) =>
        new() { Value = Guid.Parse(input) };
}