using Application.Abstractions;
using Domain.Payments;
using Domain.Primitives;

namespace Application.Payments;

public sealed record CreateCommand: BaseCommand
{
    public Currency Currency { get; init; }
    public CustomAnswer IsAutoDebit { get; init; }
    public string? PaymentMediaReference { get; init; }
}

internal static partial class CreateCommandExtensions
{
    public static Payment MapToPayment( this CreateCommand command )
    {
        return command is null ?
            throw new ArgumentNullException( nameof( command ) ) :
            new Payment(
                command.Description!,
                command.Value,
                command.Currency,
                command.IsAutoDebit,
                command.PaymentMediaReference );
    }
}
