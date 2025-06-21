using System.Net.Http.Json;
using Application.Notifications;

namespace Application.Sdk;

public class NotificationsWebApiClient( HttpClient httpClient )
{
    public async Task<NotificationsResponse?> GetNotificationAsync( Guid id )
    {
        // We're using the WebApiIdRoute to ensure that the request is routed 
        // correctly through the API Gateway (Yarp Reverse Proxy).
        var response = await httpClient.GetAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Notifications.Base )}/{id}" );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<NotificationsResponse>();
    }

    public async Task<IEnumerable<NotificationsResponse>> GetNotificationsAsync()
    {
        var response = await httpClient.GetAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Notifications.GetAll )}" );

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<NotificationsResponse>>() ?? [];
    }

    public async Task CreateNotificationAsync( CreateCommand request )
    {
        var response = await httpClient.PostAsJsonAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Notifications.Create )}", request );
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateNotificationAsync( UpdateCommand request )
    {
        var response = await httpClient.PutAsJsonAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Notifications.Update )}", request );
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteNotificationAsync( Guid id )
    {
        var response = await httpClient.DeleteAsync(
            $"{ApiEndpoints.WebApiIdRoute}/{ApiEndpoints.MapVersion( ApiEndpoints.Notifications.Base )}/{id}" );
        response.EnsureSuccessStatusCode();
    }
}

