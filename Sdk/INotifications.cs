using Application.Notifications;
using Domain.Notifications;
using Refit;

namespace Application.Sdk;

[Headers( "Authorization: Bearer" )]
public interface INotificationsApi
{
    [Get( ApiEndpoints.Notifications.GetOne )]
    Task<Notification> GetNotificationAsync( Guid id );

    [Get( ApiEndpoints.Notifications.GetAll )]
    Task<IEnumerable<Notification>> GetNotificationsAsync();

    [Post( ApiEndpoints.Notifications.Create )]
    Task<Notification> CreateNotificationAsync( CreateCommand request );

    [Put( ApiEndpoints.Notifications.Update )]
    Task<Notification> UpdateNotificationAsync( UpdateCommand request );

    [Delete( ApiEndpoints.Notifications.Delete )]
    Task DeleteNotificationAsync( Guid id );
}

