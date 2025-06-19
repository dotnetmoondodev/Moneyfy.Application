using Application.Abstractions;
using Domain.Notifications;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Notifications;

public interface IQueryOneHandler: IQueryHandler<GetByIdQuery, NotificationsResponse> { }

public sealed class GetByIdQueryHandler(
    IAppDbContext dbContext )
    : IQueryOneHandler
{
    public async Task<NotificationsResponse> Execute(
        GetByIdQuery query, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( query );

        var validator = new GetByIdQueryValidator();
        var result = await validator.ValidateAsync( query, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var notification = await dbContext.Notifications
            .Where( item => item.Id == query.Id )
            .Select( item => new NotificationsResponse()
            {
                Id = item.Id,
                Description = item.Description,
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
            .FirstOrDefaultAsync( cancellationToken ) ??
                throw new NotFoundNotificationException( query.Id );

        return notification;
    }
}

internal class GetByIdQueryValidator: QueryValidator<GetByIdQuery>
{
    public GetByIdQueryValidator() : base() { }
}
