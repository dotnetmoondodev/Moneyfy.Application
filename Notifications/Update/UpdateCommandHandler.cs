using Application.Abstractions;
using Domain.Notifications;
using Domain.Primitives;
using FluentValidation;

namespace Application.Notifications;

public interface IUpdateCommandHandler: ICommandHandler<UpdateCommand> { }

public sealed class UpdateCommandHandler(
    INotificationsRepository repository )
    : IUpdateCommandHandler
{
    public async Task Execute( UpdateCommand command, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( command );

        var validator = new UpdateCommandValidator();
        var result = await validator.ValidateAsync( command, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var notification = await repository.GetByIdAsync( command.Id, cancellationToken ) ??
            throw new NotFoundNotificationException( command.Id );

        notification.Update(
            command.Description,
            command.DateToSend,
            command.HourToSend,
            command.Frequency,
            command.Method,
            command.PaymentId,
            command.Repeatable,
            command.Enable,
            command.Email,
            command.PhoneNumber );

        await repository.UpdateAsync( notification, cancellationToken );
    }
}

internal class UpdateCommandValidator: AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator()
    {
        RuleFor( x => x.Id )
            .NotEmpty()
            .WithMessage( "Id is required." );

        RuleFor( x => x.Description )
            .NotEmpty()
            .MaximumLength( 128 )
            .WithMessage( "Description is required and must not exceed 128 characters." );

        RuleFor( x => x.DateToSend )
            .NotNull()
            .Must( x => x > DateTime.Now )
            .WithMessage( "DateToSend is required and must be a future date." );

        RuleFor( x => x.HourToSend )
            .NotNull()
            .IsInEnum()
            .WithMessage( "HourToSend is required and must be a valid CustomHours value." );

        RuleFor( x => x.Frequency )
            .NotNull()
            .IsInEnum()
            .WithMessage( "Frequency is required and must be a valid NotificationFrequency value." );

        RuleFor( x => x.Method )
            .NotNull()
            .IsInEnum()
            .WithMessage( "Method is required and must be a valid NotificationMethod value." );

        RuleFor( x => x.Repeatable )
            .NotNull()
            .IsInEnum()
            .WithMessage( "Repeatable is required and must be a valid CustomAnswer value." );

        RuleFor( x => x.Enable )
            .NotNull()
            .IsInEnum()
            .WithMessage( "Enable is required and must be a valid CustomAnswer value." );

        RuleFor( x => x.Email )
            .EmailAddress()
            .When( x => x.Method == NotificationMethod.Email )
            .WithMessage( "Email is required and must be a valid email address." );

        RuleFor( x => x.PhoneNumber )
            .Matches( @"^\+?[1-9]\d{1,14}$" )
            .When( x => x.Method == NotificationMethod.Sms || x.Method == NotificationMethod.PushNotification )
            .WithMessage( "PhoneNumber is required and must be a valid phone number." );
    }
}
