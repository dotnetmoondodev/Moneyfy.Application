using Application.Abstractions;
using Domain.Expenses;
using FluentValidation;

namespace Application.Expenses;

public interface ICreateCommandHandler: ICommandHandler<CreateCommand, Expense> { }

public sealed class CreateCommandHandler(
    IExpensesRepository repository )
    : ICreateCommandHandler
{
    public async Task<Expense> Execute( CreateCommand command, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( command );

        var validator = new CreateCommandValidator();
        var result = await validator.ValidateAsync( command, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var expense = command.MapToExpense();
        await repository.AddAsync( expense, cancellationToken );
        return expense;
    }
}

internal class CreateCommandValidator: CommandValidator<CreateCommand>
{
    public CreateCommandValidator() : base() { }
}
