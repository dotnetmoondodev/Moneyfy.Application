using Application.Abstractions;
using Domain.Primitives;
using System.ComponentModel.DataAnnotations;

namespace Application.Payments;

public sealed record UpdateCommand: BaseCommand
{
	[Required( ErrorMessage = "Id is required." )]
	public Guid Id { get; init; }

    [Required( ErrorMessage = "Currency is required." )]
    [EnumDataType( typeof( Currency ), ErrorMessage = "Invalid currency type." )]
    public required Currency Currency { get; init; }

    [Required( ErrorMessage = "IsAutoDebit is required." )]
    [Range( 0, 1, ErrorMessage = "IsAutoDebit must be either true (1) or false (0)." )]
    public required bool IsAutoDebit { get; init; }

    [Required( ErrorMessage = "PaymentMediaReference is required." )]
    [StringLength( 128, ErrorMessage = "PaymentMediaReference cannot exceed 128 characters." )]
    public required string PaymentMediaReference { get; init; }
}
