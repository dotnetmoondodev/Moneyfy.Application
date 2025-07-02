using Application.Abstractions;
using Domain.Expenses;

namespace Application.Expenses;

public interface IQueryAllHandler: IQueryHandler<IReadOnlyCollection<ExpensesResponse>> { }

internal sealed class GetAllQueryHandler(
    IExpensesRepository repository )
    : IQueryAllHandler
{
    public async Task<IReadOnlyCollection<ExpensesResponse>> Execute(
        CancellationToken cancellationToken = default )
    {
        var records = await repository.GetAllAsync( null, cancellationToken );
        return [.. records.Select( item => new ExpensesResponse()
        {
            Id = item.Id,
            Description = item.Description,
            Value = item.Value,
            CreationDate = item.CreationDate
        } )];
    }
}
