using System.Net.Http.Json;
using Application.Notifications;
using Domain.Notifications;

namespace Application.Sdk;

public class NotificationsWebApiClient( HttpClient httpClient )
{
    public async Task<Notification?> GetNotificationAsync( Guid id )
    {
        var response = await httpClient.GetAsync( ApiEndpoints.Notifications.GetOne );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Notification>();
    }

    public async Task<IEnumerable<Notification>> GetNotificationsAsync()
    {
        var response = await httpClient.GetAsync( ApiEndpoints.Notifications.GetAll );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<Notification>>() ?? [];
    }

    public async Task<Notification?> CreateNotificationAsync( CreateCommand request )
    {
        var response = await httpClient.PostAsJsonAsync( ApiEndpoints.Notifications.Create, request );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Notification>();
    }

    public async Task<Notification?> UpdateNotificationAsync( UpdateCommand request )
    {
        var response = await httpClient.PutAsJsonAsync( ApiEndpoints.Notifications.Update, request );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Notification>();
    }

    public async Task DeleteNotificationAsync( Guid id )
    {
        var response = await httpClient.DeleteAsync( ApiEndpoints.Notifications.Delete );
        response.EnsureSuccessStatusCode();
    }
}

