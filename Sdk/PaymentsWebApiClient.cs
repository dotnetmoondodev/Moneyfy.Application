using System.Net.Http.Json;
using Application.Payments;
using Domain.Payments;

namespace Application.Sdk;

public class PaymentsWebApiClient( HttpClient httpClient )
{
    public async Task<Payment?> GetPaymentAsync( Guid id )
    {
        var response = await httpClient.GetAsync( ApiEndpoints.Payments.GetOne );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Payment>();
    }

    public async Task<IEnumerable<Payment>> GetPaymentsAsync()
    {
        var response = await httpClient.GetAsync( ApiEndpoints.Payments.GetAll );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<Payment>>() ?? [];
    }

    public async Task<Payment?> CreatePaymentAsync( CreateCommand request )
    {
        var response = await httpClient.PostAsJsonAsync( ApiEndpoints.Payments.Create, request );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Payment>();
    }

    public async Task<Payment?> UpdatePaymentAsync( UpdateCommand request )
    {
        var response = await httpClient.PutAsJsonAsync( ApiEndpoints.Payments.Update, request );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Payment>();
    }

    public async Task DeletePaymentAsync( Guid id )
    {
        var response = await httpClient.DeleteAsync( ApiEndpoints.Payments.Delete );
        response.EnsureSuccessStatusCode();
    }
}

