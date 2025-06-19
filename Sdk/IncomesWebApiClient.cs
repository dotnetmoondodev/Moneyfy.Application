using System.Net.Http.Json;
using Application.Incomes;
using Domain.Incomes;

namespace Application.Sdk;

public class IncomesWebApiClient( HttpClient httpClient )
{
    public async Task<Income?> GetIncomeAsync( Guid id )
    {
        var response = await httpClient.GetAsync(
            $"{ApiEndpoints.MapVersion( ApiEndpoints.Incomes.Base )}/{id}" );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Income>();
    }

    public async Task<IEnumerable<Income>> GetIncomesAsync()
    {
        var response = await httpClient.GetAsync(
            ApiEndpoints.MapVersion( ApiEndpoints.Incomes.GetAll ) );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<Income>>() ?? [];
    }

    public async Task<Income?> CreateIncomeAsync( CreateCommand request )
    {
        var response = await httpClient.PostAsJsonAsync(
            ApiEndpoints.MapVersion( ApiEndpoints.Incomes.Create ), request );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Income>();
    }

    public async Task<Income?> UpdateIncomeAsync( UpdateCommand request )
    {
        var response = await httpClient.PutAsJsonAsync(
            ApiEndpoints.MapVersion( ApiEndpoints.Incomes.Update ), request );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Income>();
    }

    public async Task DeleteIncomeAsync( Guid id )
    {
        var response = await httpClient.DeleteAsync(
            $"{ApiEndpoints.MapVersion( ApiEndpoints.Incomes.Base )}/{id}" );

        response.EnsureSuccessStatusCode();
    }
}

