using Application.Abstractions;
using Domain.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Application.Notifications;

public interface IQueryAllHandler : IQueryHandler<IEnumerable<Notification>> { }

internal sealed class GetAllQueryHandler( 
	IAppDbContext appDbContext )
    : IQueryAllHandler
{
	public async Task<IEnumerable<Notification>> Execute( CancellationToken cancellationToken = default )
	{
		return await appDbContext.Notifications.ToListAsync( cancellationToken );
	}
}
