using Application.Abstractions;
using Domain.Notifications;
using Domain;
using FluentValidation;

namespace Application.Notifications;

public interface IQueryOneHandler: IQueryHandler<GetByIdQuery, NotificationsResponse> { }

public sealed class GetByIdQueryHandler(
    IRepository<Notification> repository )
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

        var notification = await repository.GetByIdAsync( query.Id, cancellationToken ) ??
            throw new NotFoundNotificationException( query.Id );

        return new NotificationsResponse()
        {
            Id = notification.Id,
            Description = notification.Description!,
            CreationDate = notification.CreationDate,
            DateToSend = notification.DateToSend,
            HourToSend = notification.HourToSend,
            Frequency = notification.Frequency,
            Method = notification.Method,
            PaymentId = notification.PaymentId,
            Repeatable = notification.Repeatable,
            Enable = notification.Enable,
            Email = notification.Email,
            PhoneNumber = notification.PhoneNumber
        };
    }
}

internal class GetByIdQueryValidator: QueryValidator<GetByIdQuery>
{
    public GetByIdQueryValidator() : base() { }
}
