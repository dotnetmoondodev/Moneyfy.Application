using Application.Abstractions;
using Domain.Expenses;
using Domain;

namespace Application.Expenses;

public interface IQueryAllHandler: IQueryHandler<IReadOnlyCollection<ExpensesResponse>> { }

internal sealed class GetAllQueryHandler(
    IRepository<Expense> repository )
    : IQueryAllHandler
{
    public async Task<IReadOnlyCollection<ExpensesResponse>> Execute(
        CancellationToken cancellationToken = default )
    {
        var records = await repository.GetAllAsync( cancellationToken );
        return [.. records.Select( item => new ExpensesResponse()
        {
            Id = item.Id,
            Description = item.Description,
            Value = item.Value,
            CreationDate = item.CreationDate
        } )];
    }
}
