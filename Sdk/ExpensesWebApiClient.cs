using System.Net.Http.Json;
using Application.Expenses;

namespace Application.Sdk;

public class ExpensesWebApiClient( HttpClient httpClient )
{
    public async Task<ExpensesResponse?> GetExpenseAsync( Guid id )
    {
        // We're using the WebApiIdRoute to ensure that the request is routed 
        // correctly through the API Gateway (Yarp Reverse Proxy).
        var response = await httpClient.GetAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Expenses.Base )}/{id}" );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ExpensesResponse>();
    }

    public async Task<IEnumerable<ExpensesResponse>> GetExpensesAsync()
    {
        var response = await httpClient.GetAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Expenses.GetAll )}" );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ExpensesResponse>>() ?? [];
    }

    public async Task CreateExpenseAsync( CreateCommand request )
    {
        var response = await httpClient.PostAsJsonAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Expenses.Create )}", request );
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateExpenseAsync( UpdateCommand request )
    {
        var response = await httpClient.PutAsJsonAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Expenses.Update )}", request );
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteExpenseAsync( Guid id )
    {
        var response = await httpClient.DeleteAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Expenses.Base )}/{id}" );
        response.EnsureSuccessStatusCode();
    }
}

