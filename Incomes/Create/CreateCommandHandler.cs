using Application.Abstractions;
using Domain.Incomes;
using Domain;
using FluentValidation;

namespace Application.Incomes;

public interface ICreateCommandHandler: ICommandHandler<CreateCommand> { }

public sealed class CreateCommandHandler(
    IRepository<Income> repository )
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

        await repository.AddAsync( command.MapToIncome(), cancellationToken );
    }
}

internal class CreateCommandValidator: CommandValidator<CreateCommand>
{
    public CreateCommandValidator() : base()
    {
        RuleFor( x => x.Source )
            .NotEmpty()
            .WithMessage( "Source is required." );

        RuleFor( x => x.WithHolding )
            .GreaterThanOrEqualTo( 0 )
            .WithMessage( "WithHolding must be a positive number." );
    }
}
