using Application.Abstractions;
using Domain.Notifications;
using Domain;

namespace Application.Notifications;

public interface IQueryAllHandler: IQueryHandler<IReadOnlyCollection<NotificationsResponse>> { }

internal sealed class GetAllQueryHandler(
    IRepository<Notification> repository )
    : IQueryAllHandler
{
    public async Task<IReadOnlyCollection<NotificationsResponse>> Execute(
        CancellationToken cancellationToken = default )
    {
        var records = await repository.GetAllAsync( null, cancellationToken );
        return [.. records.Select( item => new NotificationsResponse()
        {
            Id = item.Id,
            Description = item.Description!,
            CreationDate = item.CreationDate,
            DateToSend = item.DateToSend,
            HourToSend = item.HourToSend,
            Frequency = item.Frequency,
            Method = item.Method,
            PaymentId = item.PaymentId,
            Repeatable = item.Repeatable,
            Enable = item.Enable,
            Email = item.Email,
            PhoneNumber = item.PhoneNumber
        } )];
    }
}
