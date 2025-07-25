using Application.Abstractions;
using Domain.Payments;
using Domain;
using FluentValidation;

namespace Application.Payments;

public interface IUpdateCommandHandler: ICommandHandler<UpdateCommand> { }

public sealed class UpdateCommandHandler(
    IRepository<Payment> repository )
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

        var payment = await repository.GetByIdAsync( command.Id, cancellationToken ) ??
            throw new NotFoundPaymentException( command.Id );

        payment.Update(
            command.Description!,
            command.Value,
            command.Currency,
            command.IsAutoDebit,
            command.PaymentMediaReference );

        await repository.UpdateAsync( payment, cancellationToken );
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
            .IsInEnum()
            .WithMessage( "Is Auto Debit is required and must be a valid enum value." );

        RuleFor( x => x.PaymentMediaReference )
            .NotEmpty()
            .MaximumLength( 128 )
            .WithMessage( "Payment Method is required and must not exceed 128 characters." );
    }
}
