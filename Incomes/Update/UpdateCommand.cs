using Application.Abstractions;

namespace Application.Incomes;

public sealed record UpdateCommand: BaseCommand
{
    public Guid Id { get; init; }
    public string? Source { get; init; }
    public decimal WithHolding { get; init; }
}
