using Application.Abstractions;
using Domain.Primitives;

namespace Application.Payments;

public sealed record UpdateCommand: BaseCommand
{
    public Guid Id { get; init; }
    public Currency Currency { get; init; }
    public CustomAnswer IsAutoDebit { get; init; }
    public string? PaymentMediaReference { get; init; }
}
