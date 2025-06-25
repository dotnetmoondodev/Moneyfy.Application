namespace Application.Abstractions;

public abstract record BaseCommand
{
    public string? Description { get; init; }
    public decimal Value { get; init; }
}
