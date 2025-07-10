using Application.Abstractions;
using Domain.Expenses;
using Domain;
using FluentValidation;

namespace Application.Expenses;

public interface ICreateCommandHandler: ICommandHandler<CreateCommand> { }

public sealed class CreateCommandHandler(
    IRepository<Expense> repository )
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

        await repository.AddAsync( command.MapToExpense(), cancellationToken );
    }
}

internal class CreateCommandValidator: CommandValidator<CreateCommand>
{
    public CreateCommandValidator() : base() { }
}
