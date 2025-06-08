using Application.Abstractions;
using Domain.Expenses;
using FluentValidation;

namespace Application.Expenses;

public interface IUpdateCommandHandler: ICommandHandler<UpdateCommand, Expense> { }

public sealed class UpdateCommandHandler(
    IExpensesRepository repository )
    : IUpdateCommandHandler
{
    public async Task<Expense> Execute( UpdateCommand command, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( command );

        var validator = new UpdateCommandValidator();
        var result = await validator.ValidateAsync( command, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var expense = await repository.GetByIdAsync( command.Id, cancellationToken ) ??
            throw new NotFoundExpenseException( command.Id );

        expense.Update(
            command.Description,
            command.Value );

        await repository.UpdateAsync( expense, cancellationToken );
        return expense;
    }
}

internal class UpdateCommandValidator: CommandValidator<UpdateCommand>
{
    public UpdateCommandValidator() : base()
    {
        RuleFor( x => x.Id )
            .NotEmpty()
            .WithMessage( "Id is required." );
    }
}
