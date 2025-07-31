using Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Payments;

public interface IAppDbContext
{
    DbSet<Payment> Payments { get; set; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync( CancellationToken cancellationToken = default );
}
