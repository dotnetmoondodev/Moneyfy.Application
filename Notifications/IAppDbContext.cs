using Domain.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Notifications;

public interface IAppDbContext
{
    DbSet<Notification> Notifications { get; set; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync( CancellationToken cancellationToken = default );
}
