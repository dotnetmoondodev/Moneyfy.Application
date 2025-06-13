using Application.Expenses;
using Domain.Expenses;
using Refit;

namespace Application.Sdk;

[Headers( "Authorization: Bearer" )]
public interface IExpensesApi
{
    [Get( ApiEndpoints.Expenses.GetOne )]
    Task<Expense> GetExpenseAsync( Guid id );

    [Get( ApiEndpoints.Expenses.GetAll )]
    Task<IEnumerable<Expense>> GetExpensesAsync();

    [Post( ApiEndpoints.Expenses.Create )]
    Task<Expense> CreateExpenseAsync( CreateCommand request );

    [Put( ApiEndpoints.Expenses.Update )]
    Task<Expense> UpdateExpenseAsync( UpdateCommand request );

    [Delete( ApiEndpoints.Expenses.Delete )]
    Task DeleteExpenseAsync( Guid id );
}

