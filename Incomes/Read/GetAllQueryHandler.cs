using Application.Abstractions;
using Domain.Incomes;
using Microsoft.EntityFrameworkCore;

namespace Application.Incomes;

public interface IQueryAllHandler : IQueryHandler<IEnumerable<Income>> { }

internal sealed class GetAllQueryHandler( 
	IAppDbContext appDbContext )
    : IQueryAllHandler
{
	public async Task<IEnumerable<Income>> Execute( CancellationToken cancellationToken = default )
	{
		return await appDbContext.Incomes.ToListAsync( cancellationToken );
	}
}
