using Application.Abstractions;
using Domain.Payments;
using Microsoft.EntityFrameworkCore;

namespace Application.Payments;

public interface IQueryAllHandler : IQueryHandler<IEnumerable<Payment>> { }

internal sealed class GetAllQueryHandler( 
	IAppDbContext appDbContext )
    : IQueryAllHandler
{
	public async Task<IEnumerable<Payment>> Execute( CancellationToken cancellationToken = default )
	{
		return await appDbContext.Payments.ToListAsync( cancellationToken );
	}
}
