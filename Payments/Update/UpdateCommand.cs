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
    [EnumDataType( typeof( CustomAnswer ), ErrorMessage = "Invalid answer type." )]
    public required CustomAnswer IsAutoDebit { get; init; }

    [Required( ErrorMessage = "PaymentMediaReference is required." )]
    [StringLength( 128, ErrorMessage = "PaymentMediaReference cannot exceed 128 characters." )]
    public required string PaymentMediaReference { get; init; }
}
