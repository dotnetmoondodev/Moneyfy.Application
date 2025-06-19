using Application.Abstractions;
using Domain.Primitives;

namespace Application.Payments;

public sealed record GetByIdQuery: BaseQuery { }

public sealed record PaymentsResponse
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public decimal Value { get; set; }
    public Currency Currency { get; set; }
    public bool IsAutoDebit { get; set; }
    public string? PaymentMediaReference { get; set; }
}
