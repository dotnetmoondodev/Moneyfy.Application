using Application.Abstractions;
using Domain.Notifications;
using FluentValidation;

namespace Application.Notifications;

public interface IQueryOneHandler: IQueryHandler<GetByIdQuery, Notification> { }

public sealed class GetByIdQueryHandler(
    INotificationsRepository repository )
    : IQueryOneHandler
{
    public async Task<Notification> Execute( GetByIdQuery query, CancellationToken cancellationToken = default )
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

        return notification;
    }
}

internal class GetByIdQueryValidator: QueryValidator<GetByIdQuery>
{
    public GetByIdQueryValidator() : base() { }
}
