using Domain.Expenses;
using Domain.Incomes;
using Domain.Notifications;
using Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Abstractions;

public interface IAppDbContext
{
	DbSet<Expense> Expenses { get; set; }
    DbSet<Income> Incomes { get; set; }
    DbSet<Notification> Notifications { get; set; }
    DbSet<Payment> Payments { get; set; }

    DatabaseFacade Database { get; }

	Task<int> SaveChangesAsync( CancellationToken cancellationToken = default );
}
