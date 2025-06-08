using Application.Abstractions;
using Domain.Incomes;
using FluentValidation;

namespace Application.Incomes;

public interface IQueryOneHandler: IQueryHandler<GetByIdQuery, Income> { }

public sealed class GetByIdQueryHandler(
    IIncomesRepository repository )
    : IQueryOneHandler
{
    public async Task<Income> Execute( GetByIdQuery query, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( query );

        var validator = new GetByIdQueryValidator();
        var result = await validator.ValidateAsync( query, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var income = await repository.GetByIdAsync( query.Id, cancellationToken ) ??
            throw new NotFoundIncomeException( query.Id );

        return income;
    }
}

internal class GetByIdQueryValidator: QueryValidator<GetByIdQuery>
{
    public GetByIdQueryValidator() : base() { }
}
