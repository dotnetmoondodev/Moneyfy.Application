using Application.Abstractions;

namespace Application.Expenses;

public sealed record UpdateCommand: BaseCommand
{
    public Guid Id { get; init; }
}
