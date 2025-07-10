using Application.Abstractions;
using Domain.Notifications;
using Domain;
using FluentValidation;

namespace Application.Notifications;

public interface IDeleteCommandHandler: ICommandHandler<DeleteCommand> { }

public sealed class DeleteCommandHandler(
    IRepository<Notification> repository )
    : IDeleteCommandHandler
{
    public async Task Execute( DeleteCommand command, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( command );

        var validator = new DeleteCommandValidator();
        var result = await validator.ValidateAsync( command, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var payment = await repository.GetByIdAsync( command.Id, cancellationToken ) ??
            throw new NotFoundNotificationException( command.Id );

        await repository.DeleteAsync( payment, cancellationToken );
    }
}

internal class DeleteCommandValidator: QueryValidator<DeleteCommand>
{
    public DeleteCommandValidator() : base() { }
}
