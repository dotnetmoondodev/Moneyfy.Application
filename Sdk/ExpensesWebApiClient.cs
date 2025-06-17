using System.Net.Http.Json;
using Application.Expenses;
using Domain.Expenses;

namespace Application.Sdk;

public class ExpensesWebApiClient( HttpClient httpClient )
{
    public async Task<Expense?> GetExpenseAsync( Guid id )
    {
        var response = await httpClient.GetAsync( ApiEndpoints.Expenses.GetOne );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Expense>();
    }

    public async Task<IEnumerable<Expense>> GetExpensesAsync()
    {
        var response = await httpClient.GetAsync( ApiEndpoints.Expenses.GetAll );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<Expense>>() ?? [];
    }

    public async Task<Expense?> CreateExpenseAsync( CreateCommand request )
    {
        var response = await httpClient.PostAsJsonAsync( ApiEndpoints.Expenses.Create, request );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Expense>();
    }

    public async Task<Expense?> UpdateExpenseAsync( UpdateCommand request )
    {
        var response = await httpClient.PutAsJsonAsync( ApiEndpoints.Expenses.Update, request );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Expense>();
    }

    public async Task DeleteExpenseAsync( Guid id )
    {
        var response = await httpClient.DeleteAsync( ApiEndpoints.Expenses.Delete );
        response.EnsureSuccessStatusCode();
    }
}

