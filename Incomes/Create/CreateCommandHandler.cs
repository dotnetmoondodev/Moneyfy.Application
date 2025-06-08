using Application.Abstractions;
using Domain.Incomes;
using FluentValidation;

namespace Application.Incomes;

public interface ICreateCommandHandler: ICommandHandler<CreateCommand, Income> { }

public sealed class CreateCommandHandler(
    IIncomesRepository repository )
    : ICreateCommandHandler
{
    public async Task<Income> Execute( CreateCommand command, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( command );

        var validator = new CreateCommandValidator();
        var result = await validator.ValidateAsync( command, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var income = command.MapToIncome();
        await repository.AddAsync( income, cancellationToken );
        return income;
    }
}

internal class CreateCommandValidator: CommandValidator<CreateCommand>
{
    public CreateCommandValidator(): base()
    {
        RuleFor( x => x.Source )
            .NotEmpty()
            .WithMessage( "Source is required." );

        RuleFor( x => x.WithHolding )
            .GreaterThan( 0 )
            .WithMessage( "WithHolding must be a positive number." );
    }
}
