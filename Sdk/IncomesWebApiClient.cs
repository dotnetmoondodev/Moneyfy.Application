using System.Net.Http.Json;
using Application.Incomes;

namespace Application.Sdk;

public class IncomesWebApiClient( HttpClient httpClient )
{
    public async Task<IncomesResponse?> GetIncomeAsync( Guid id )
    {
        // We're using the WebApiIdRoute to ensure that the request is routed 
        // correctly through the API Gateway (Yarp Reverse Proxy).
        var response = await httpClient.GetAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Incomes.Base )}/{id}" );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IncomesResponse>();
    }

    public async Task<IEnumerable<IncomesResponse>> GetIncomesAsync()
    {
        var response = await httpClient.GetAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Incomes.GetAll )}" );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<IncomesResponse>>() ?? [];
    }

    public async Task CreateIncomeAsync( CreateCommand request )
    {
        var response = await httpClient.PostAsJsonAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Incomes.Create )}", request );
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateIncomeAsync( UpdateCommand request )
    {
        var response = await httpClient.PutAsJsonAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Incomes.Update )}", request );
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteIncomeAsync( Guid id )
    {
        var response = await httpClient.DeleteAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Incomes.Base )}/{id}" );
        response.EnsureSuccessStatusCode();
    }
}

