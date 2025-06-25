namespace Application.Abstractions;

public abstract record BaseQuery
{
    public Guid Id { get; init; }
}
