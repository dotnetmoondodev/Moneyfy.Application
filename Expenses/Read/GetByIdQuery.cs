using System.ComponentModel.DataAnnotations;
using Application.Abstractions;

namespace Application.Expenses;

public sealed record GetByIdQuery: BaseQuery { }

public sealed record ExpensesResponse
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }

    [Required( ErrorMessage = "Description is required." )]
    [StringLength( 128, ErrorMessage = "Description cannot exceed 128 characters." )]
    public required string? Description { get; set; }

    [Required( ErrorMessage = "Value is required." )]
    [Range( 1, double.MaxValue, ErrorMessage = "Value must be greater than $0." )]
    public required decimal Value { get; set; }
}
