using Application.Abstractions;
using Domain.Incomes;
using FluentValidation;

namespace Application.Incomes;

public interface IUpdateCommandHandler: ICommandHandler<UpdateCommand, Income> { }

public sealed class UpdateCommandHandler(
    IIncomesRepository repository )
    : IUpdateCommandHandler
{
    public async Task<Income> Execute( UpdateCommand command, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( command );

        var validator = new UpdateCommandValidator();
        var result = await validator.ValidateAsync( command, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var income = await repository.GetByIdAsync( command.Id, cancellationToken ) ??
            throw new NotFoundIncomeException( command.Id );

        income.Update(
            command.Description,
            command.Value,
            command.Source,
            command.WithHolding );

        await repository.UpdateAsync( income, cancellationToken );
        return income;
    }
}

internal class UpdateCommandValidator: CommandValidator<UpdateCommand>
{
    public UpdateCommandValidator() : base()
    {
        RuleFor( x => x.Id )
            .NotEmpty()
            .WithMessage( "Id is required." );

        RuleFor( x => x.Source )
            .NotEmpty()
            .WithMessage( "Source is required." );

        RuleFor( x => x.WithHolding )
            .GreaterThan( 0 )
            .WithMessage( "WithHolding must be a positive number." );
    }
}
