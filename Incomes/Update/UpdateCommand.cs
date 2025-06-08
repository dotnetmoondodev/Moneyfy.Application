using Application.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Application.Incomes;

public sealed record UpdateCommand: BaseCommand
{
	[Required( ErrorMessage = "Id is required." )]
	public Guid Id { get; init; }

    [Required( ErrorMessage = "Source is required." )]
    [StringLength( 128, ErrorMessage = "Source cannot exceed 128 characters." )]
    public required string Source { get; init; }

    [Required( ErrorMessage = "WithHolding is required." )]
    [Range( 0, double.MaxValue, ErrorMessage = "WithHolding must be a positive number." )]
    public required decimal WithHolding { get; init; }
}
