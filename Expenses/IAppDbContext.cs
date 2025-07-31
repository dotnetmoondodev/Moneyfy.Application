using Domain.Expenses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Expenses;

public interface IAppDbContext
{
    DbSet<Expense> Expenses { get; set; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync( CancellationToken cancellationToken = default );
}
