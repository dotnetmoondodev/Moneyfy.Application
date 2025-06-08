using System.ComponentModel.DataAnnotations;

namespace Application.Abstractions;

public abstract record BaseCommand
{
    [Required( ErrorMessage = "Description is required." )]
    [StringLength( 128, ErrorMessage = "Description cannot exceed 128 characters." )]
    public required string Description { get; init; }

    [Required( ErrorMessage = "Value is required." )]
    [Range( 0, double.MaxValue, ErrorMessage = "Value must be a positive number." )]
    public required decimal Value { get; init; }
}
