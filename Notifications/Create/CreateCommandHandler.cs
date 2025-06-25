using Application.Abstractions;
using Domain.Notifications;
using Domain.Primitives;
using FluentValidation;

namespace Application.Notifications;

public interface ICreateCommandHandler: ICommandHandler<CreateCommand> { }

public sealed class CreateCommandHandler(
    INotificationsRepository repository )
    : ICreateCommandHandler
{
    public async Task Execute( CreateCommand command, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( command );

        var validator = new CreateCommandValidator();
        var result = await validator.ValidateAsync( command, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        await repository.AddAsync( command.MapToNotification(), cancellationToken );
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
            .Must( x => x > DateTime.Now )
            .WithMessage( "Date to Send is required and must be a future date." );

        RuleFor( x => x.HourToSend )
            .NotNull()
            .IsInEnum()
            .WithMessage( "Hour to Send is required and must be a valid CustomHours value." );

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
            .Matches( @"^\+?[1-9]\d{9,14}$" )
            .When( x => x.Method == NotificationMethod.Sms || x.Method == NotificationMethod.PushNotification )
            .WithMessage( "PhoneNumber is required and must be a valid phone number." );
    }
}
