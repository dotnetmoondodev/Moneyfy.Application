using Domain.Incomes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Incomes;

public interface IAppDbContext
{
    DbSet<Income> Incomes { get; set; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync( CancellationToken cancellationToken = default );
}
