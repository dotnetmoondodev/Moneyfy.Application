using System.Net.Http.Json;
using Application.Payments;

namespace Application.Sdk;

public class PaymentsWebApiClient( HttpClient httpClient )
{
    public async Task<PaymentsResponse?> GetPaymentAsync( Guid id )
    {
        // We're using the WebApiIdRoute to ensure that the request is routed 
        // correctly through the API Gateway (Yarp Reverse Proxy).
        var response = await httpClient.GetAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Payments.Base )}/{id}" );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PaymentsResponse>();
    }

    public async Task<IEnumerable<PaymentsResponse>> GetPaymentsAsync()
    {
        var response = await httpClient.GetAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Payments.GetAll )}" );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<PaymentsResponse>>() ?? [];
    }

    public async Task CreatePaymentAsync( CreateCommand request )
    {
        var response = await httpClient.PostAsJsonAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Payments.Create )}", request );
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdatePaymentAsync( UpdateCommand request )
    {
        var response = await httpClient.PutAsJsonAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Payments.Update )}", request );
        response.EnsureSuccessStatusCode();
    }

    public async Task DeletePaymentAsync( Guid id )
    {
        var response = await httpClient.DeleteAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Payments.Base )}/{id}" );
        response.EnsureSuccessStatusCode();
    }
}

