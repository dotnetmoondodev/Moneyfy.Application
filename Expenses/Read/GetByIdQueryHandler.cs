using Application.Abstractions;
using Domain.Expenses;
using FluentValidation;

namespace Application.Expenses;

public interface IQueryOneHandler: IQueryHandler<GetByIdQuery, Expense> { }

public sealed class GetByIdQueryHandler(
    IExpensesRepository repository )
    : IQueryOneHandler
{
    public async Task<Expense> Execute( GetByIdQuery query, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( query );

        var validator = new GetByIdQueryValidator();
        var result = await validator.ValidateAsync( query, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var expense = await repository.GetByIdAsync( query.Id, cancellationToken ) ??
            throw new NotFoundExpenseException( query.Id );

        return expense;
    }
}

internal class GetByIdQueryValidator: QueryValidator<GetByIdQuery>
{
    public GetByIdQueryValidator() : base() { }
}
