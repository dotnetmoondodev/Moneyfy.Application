using Application.Abstractions;
using Domain.Incomes;
using System.ComponentModel.DataAnnotations;

namespace Application.Incomes;

public sealed record CreateCommand: BaseCommand
{
    [Required( ErrorMessage = "Source is required." )]
    [StringLength( 128, ErrorMessage = "Source cannot exceed 128 characters." )]
    public required string Source { get; init; }

    [Required( ErrorMessage = "WithHolding is required." )]
    [Range( 0, double.MaxValue, ErrorMessage = "WithHolding must be a positive number." )]
    public required decimal WithHolding { get; init; }
}

internal static partial class CreateCommandExtensions
{
    public static Income MapToIncome( this CreateCommand command )
    {
        return command is null ?
            throw new ArgumentNullException( nameof( command ) ) :
            new Income(
                command.Description,
                command.Value,
                command.Source,
                command.WithHolding );
    }
}
