using Application.Abstractions;
using Domain.Expenses;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Expenses;

public interface IQueryOneHandler: IQueryHandler<GetByIdQuery, ExpensesResponse> { }

public sealed class GetByIdQueryHandler(
    IAppDbContext dbContext )
    : IQueryOneHandler
{
    public async Task<ExpensesResponse> Execute(
        GetByIdQuery query, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( query );

        var validator = new GetByIdQueryValidator();
        var result = await validator.ValidateAsync( query, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var expense = await dbContext.Expenses
            .Where( item => item.Id == query.Id )
            .Select( item => new ExpensesResponse()
            {
                Id = item.Id,
                Description = item.Description,
                Value = item.Value,
                CreationDate = item.CreationDate
            } )
            .FirstOrDefaultAsync( cancellationToken ) ??
                throw new NotFoundExpenseException( query.Id );

        return expense;
    }
}

internal class GetByIdQueryValidator: QueryValidator<GetByIdQuery>
{
    public GetByIdQueryValidator() : base() { }
}
