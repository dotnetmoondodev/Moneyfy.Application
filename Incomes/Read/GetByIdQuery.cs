using Application.Abstractions;

namespace Application.Incomes;

public sealed record GetByIdQuery: BaseQuery { }

public sealed record IncomesResponse
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    public decimal Value { get; set; }
    public string? Source { get; set; }
    public decimal Withholding { get; set; }
}
