using Application.Abstractions;
using Domain.Notifications;
using Domain.Primitives;
using FluentValidation;

namespace Application.Notifications;

public interface ICreateCommandHandler: ICommandHandler<CreateCommand, Notification> { }

public sealed class CreateCommandHandler(
    INotificationsRepository repository )
    : ICreateCommandHandler
{
    public async Task<Notification> Execute( CreateCommand command, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( command );

        var validator = new CreateCommandValidator();
        var result = await validator.ValidateAsync( command, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var notification = command.MapToNotification();
        await repository.AddAsync( notification, cancellationToken );
        return notification;
    }
}

internal class CreateCommandValidator: AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor( x => x.Description )
            .NotEmpty()
            .MaximumLength( 128 )
            .WithMessage( "Description is required and must not exceed 128 characters." );

        RuleFor( x => x.DateToSend )
            .NotNull()
            .Must( x => x > DateTimeOffset.MinValue )
            .WithMessage( "DateToSend is required and must be a valid DateTimeOffset." );

        RuleFor( x => x.HourToSend )
            .NotNull()
            .InclusiveBetween( 0, 23 )
            .WithMessage( "HourToSend is required and must be between 0 and 23." );

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
            .InclusiveBetween( false, true )
            .WithMessage( "Repeatable is required and must be either true or false." );

        RuleFor( x => x.Enable )
            .NotNull()
            .InclusiveBetween( false, true )
            .WithMessage( "Enable is required and must be either true or false." );

        RuleFor( x => x.Email )
            .EmailAddress()
            .When( x => x.Method == NotificationMethod.Email )
            .WithMessage( "Email is required and must be a valid email address." );

        RuleFor( x => x.PhoneNumber )
            .Matches( @"^\+?[1-9]\d{1,14}$" )
            .When( x => x.Method == NotificationMethod.Sms )
            .WithMessage( "PhoneNumber is required and must be a valid phone number." );
    }
}
