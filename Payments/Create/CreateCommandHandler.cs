using Application.Abstractions;
using Domain.Payments;
using Domain;
using FluentValidation;

namespace Application.Payments;

public interface ICreateCommandHandler: ICommandHandler<CreateCommand> { }

public sealed class CreateCommandHandler(
    IRepository<Payment> repository )
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

        await repository.AddAsync( command.MapToPayment(), cancellationToken );
    }
}

internal class CreateCommandValidator: CommandValidator<CreateCommand>
{
    public CreateCommandValidator() : base()
    {
        RuleFor( x => x.Currency )
            .NotNull()
            .IsInEnum()
            .WithMessage( "Currency is required and must be a valid enum value." );

        RuleFor( x => x.IsAutoDebit )
            .NotNull()
            .IsInEnum()
            .WithMessage( "Is Auto Debit is required and must be a valid enum value." );

        RuleFor( x => x.PaymentMediaReference )
            .NotEmpty()
            .MaximumLength( 128 )
            .WithMessage( "Payment Method is required and must not exceed 128 characters." );
    }
}
