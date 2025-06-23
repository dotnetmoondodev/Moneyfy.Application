using Application.Abstractions;

namespace Application.Expenses;

public sealed record GetByIdQuery: BaseQuery { }

public sealed record ExpensesResponse
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    public decimal Value { get; set; }
}
