using Application.Abstractions;
using Domain.Incomes;

namespace Application.Incomes;

public sealed record CreateCommand: BaseCommand
{
    public string? Source { get; init; }
    public decimal WithHolding { get; init; }
}

internal static partial class CreateCommandExtensions
{
    public static Income MapToIncome( this CreateCommand command )
    {
        return command is null ?
            throw new ArgumentNullException( nameof( command ) ) :
            new Income(
                command.Description!,
                command.Value,
                command.Source,
                command.WithHolding );
    }
}
