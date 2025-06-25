using System.ComponentModel.DataAnnotations;
using Application.Abstractions;
using Domain.Primitives;

namespace Application.Payments;

public sealed record GetByIdQuery: BaseQuery { }

public sealed record PaymentsResponse
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }

    [Required( ErrorMessage = "Description is required." )]
    [StringLength( 128, ErrorMessage = "Description cannot exceed 128 characters." )]
    public required string Description { get; set; }

    [Required( ErrorMessage = "Value is required." )]
    [Range( 1, double.MaxValue, ErrorMessage = "Value must be greater than $0." )]
    public required decimal Value { get; set; }

    [Required( ErrorMessage = "Currency is required." )]
    [EnumDataType( typeof( Currency ), ErrorMessage = "Invalid currency type." )]
    public required Currency Currency { get; set; }

    [Required( ErrorMessage = "Is Auto Debit is required." )]
    [EnumDataType( typeof( CustomAnswer ), ErrorMessage = "Invalid answer type." )]
    public required CustomAnswer IsAutoDebit { get; set; }

    [Required( ErrorMessage = "Payment method is required." )]
    [StringLength( 128, ErrorMessage = "Payment method cannot exceed 128 characters." )]
    public required string PaymentMediaReference { get; set; }
}
