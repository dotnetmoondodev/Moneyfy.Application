using Application.Abstractions;
using Domain.Payments;
using FluentValidation;

namespace Application.Payments;

public interface IUpdateCommandHandler: ICommandHandler<UpdateCommand, Payment> { }

public sealed class UpdateCommandHandler(
    IPaymentsRepository repository )
    : IUpdateCommandHandler
{
    public async Task<Payment> Execute( UpdateCommand command, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( command );

        var validator = new UpdateCommandValidator();
        var result = await validator.ValidateAsync( command, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var payment = await repository.GetByIdAsync( command.Id, cancellationToken ) ??
            throw new NotFoundPaymentException( command.Id );

        payment.Update(
            command.Description,
            command.Value,
            command.Currency,
            command.IsAutoDebit,
            command.PaymentMediaReference );

        await repository.UpdateAsync( payment, cancellationToken );
        return payment;
    }
}

internal class UpdateCommandValidator: CommandValidator<UpdateCommand>
{
    public UpdateCommandValidator() : base()
    {
        RuleFor( x => x.Id )
            .NotEmpty()
            .WithMessage( "Id is required." );

        RuleFor( x => x.Currency )
            .NotNull()
            .IsInEnum()
            .WithMessage( "Currency is required and must be a valid enum value." );

        RuleFor( x => x.IsAutoDebit )
            .NotNull()
            .InclusiveBetween( false, true )
            .WithMessage( "IsAutoDebit must be either true (1) or false (0)." );

        RuleFor( x => x.PaymentMediaReference )
            .NotEmpty()
            .MaximumLength( 128 )
            .WithMessage( "PaymentMediaReference is required and must not exceed 128 characters." );
    }
}
