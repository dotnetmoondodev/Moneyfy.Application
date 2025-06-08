using Application.Abstractions;
using Domain.Expenses;
using Microsoft.EntityFrameworkCore;

namespace Application.Expenses;

public interface IQueryAllHandler : IQueryHandler<IEnumerable<Expense>> { }

internal sealed class GetAllQueryHandler( 
	IAppDbContext appDbContext )
    : IQueryAllHandler
{
	public async Task<IEnumerable<Expense>> Execute( CancellationToken cancellationToken = default )
	{
		return await appDbContext.Expenses.ToListAsync( cancellationToken );
	}
}
