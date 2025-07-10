using Application.Abstractions;
using Domain.Expenses;
using Domain;
using FluentValidation;

namespace Application.Expenses;

public interface IDeleteCommandHandler: ICommandHandler<DeleteCommand> { }

public sealed class DeleteCommandHandler(
    IRepository<Expense> repository )
    : IDeleteCommandHandler
{
    public async Task Execute( DeleteCommand command, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( command );

        var validator = new DeleteCommandValidator();
        var result = await validator.ValidateAsync( command, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var expense = await repository.GetByIdAsync( command.Id, cancellationToken ) ??
            throw new NotFoundExpenseException( command.Id );

        await repository.DeleteAsync( expense, cancellationToken );
    }
}

internal class DeleteCommandValidator: QueryValidator<DeleteCommand>
{
    public DeleteCommandValidator() : base() { }
}
