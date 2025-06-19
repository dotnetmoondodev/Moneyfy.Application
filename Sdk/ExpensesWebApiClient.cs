using System.Net.Http.Json;
using Application.Expenses;
using Domain.Expenses;

namespace Application.Sdk;

public class ExpensesWebApiClient( HttpClient httpClient )
{
    public async Task<ExpensesResponse?> GetExpenseAsync( Guid id )
    {
        var response = await httpClient.GetAsync(
            $"{ApiEndpoints.MapVersion( ApiEndpoints.Expenses.Base )}/{id}" );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ExpensesResponse>();
    }

    public async Task<IEnumerable<ExpensesResponse>> GetExpensesAsync()
    {
        var response = await httpClient.GetAsync(
            ApiEndpoints.MapVersion( ApiEndpoints.Expenses.GetAll ) );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ExpensesResponse>>() ?? [];
    }

    public async Task<Expense?> CreateExpenseAsync( CreateCommand request )
    {
        var response = await httpClient.PostAsJsonAsync(
            ApiEndpoints.MapVersion( ApiEndpoints.Expenses.Create ), request );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Expense>();
    }

    public async Task<Expense?> UpdateExpenseAsync( UpdateCommand request )
    {
        var response = await httpClient.PutAsJsonAsync(
            ApiEndpoints.MapVersion( ApiEndpoints.Expenses.Update ), request );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Expense>();
    }

    public async Task DeleteExpenseAsync( Guid id )
    {
        var response = await httpClient.DeleteAsync(
            $"{ApiEndpoints.MapVersion( ApiEndpoints.Expenses.Base )}/{id}" );

        response.EnsureSuccessStatusCode();
    }
}

