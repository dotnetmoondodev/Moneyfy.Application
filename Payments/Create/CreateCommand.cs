using Application.Abstractions;
using Domain.Payments;
using Domain.Primitives;
using System.ComponentModel.DataAnnotations;

namespace Application.Payments;

public sealed record CreateCommand: BaseCommand
{
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

internal static partial class CreateCommandExtensions
{
    public static Payment MapToPayment( this CreateCommand command )
    {
        return command is null ?
            throw new ArgumentNullException( nameof( command ) ) :
            new Payment(
                command.Description,
                command.Value,
                command.Currency,
                command.IsAutoDebit,
                command.PaymentMediaReference );
    }
}
