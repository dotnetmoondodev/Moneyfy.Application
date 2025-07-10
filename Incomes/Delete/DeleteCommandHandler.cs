using Application.Abstractions;
using Domain.Incomes;
using Domain;
using FluentValidation;

namespace Application.Incomes;

public interface IDeleteCommandHandler: ICommandHandler<DeleteCommand> { }

public sealed class DeleteCommandHandler(
    IRepository<Income> repository )
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

        var income = await repository.GetByIdAsync( command.Id, cancellationToken ) ??
            throw new NotFoundIncomeException( command.Id );

        await repository.DeleteAsync( income, cancellationToken );
    }
}

internal class DeleteCommandValidator: QueryValidator<DeleteCommand>
{
    public DeleteCommandValidator() : base() { }
}
