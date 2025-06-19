using Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Application.Expenses;

public interface IQueryAllHandler: IQueryHandler<IEnumerable<ExpensesResponse>> { }

internal sealed class GetAllQueryHandler(
    IAppDbContext dbContext )
    : IQueryAllHandler
{
    public async Task<IEnumerable<ExpensesResponse>> Execute(
        CancellationToken cancellationToken = default )
    {
        return await dbContext.Expenses
            .Select( item => new ExpensesResponse()
            {
                Id = item.Id,
                Description = item.Description,
                Value = item.Value,
                CreationDate = item.CreationDate
            } )
            .ToListAsync( cancellationToken );
    }
}
