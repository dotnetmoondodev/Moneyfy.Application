using Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Application.Notifications;

public interface IQueryAllHandler: IQueryHandler<IEnumerable<NotificationsResponse>> { }

internal sealed class GetAllQueryHandler(
    IAppDbContext dbContext )
    : IQueryAllHandler
{
    public async Task<IEnumerable<NotificationsResponse>> Execute(
        CancellationToken cancellationToken = default )
    {
        return await dbContext.Notifications
            .Select( item => new NotificationsResponse()
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
            } )
            .ToListAsync( cancellationToken );
    }
}
