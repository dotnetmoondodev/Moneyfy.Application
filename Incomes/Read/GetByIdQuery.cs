using System.ComponentModel.DataAnnotations;
using Application.Abstractions;

namespace Application.Incomes;

public sealed record GetByIdQuery: BaseQuery { }

public sealed record IncomesResponse
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }

    [Required( ErrorMessage = "Description is required." )]
    [StringLength( 128, ErrorMessage = "Description cannot exceed 128 characters." )]
    public required string Description { get; set; }

    [Required( ErrorMessage = "Value is required." )]
    [Range( 1, double.MaxValue, ErrorMessage = "Value must be greater than $0." )]
    public required decimal Value { get; set; }

    [Required( ErrorMessage = "Source is required." )]
    [StringLength( 128, ErrorMessage = "Source cannot exceed 128 characters." )]
    public required string Source { get; set; }

    [Required( ErrorMessage = "WithHolding is required." )]
    [Range( 0, double.MaxValue, ErrorMessage = "WithHolding must be a positive number." )]
    public required decimal Withholding { get; set; }
}
