using Application.Abstractions;
using Domain.Incomes;
using Domain;
using FluentValidation;

namespace Application.Incomes;

public interface IQueryOneHandler: IQueryHandler<GetByIdQuery, IncomesResponse> { }

public sealed class GetByIdQueryHandler(
    IRepository<Income> repository )
    : IQueryOneHandler
{
    public async Task<IncomesResponse> Execute(
        GetByIdQuery query, CancellationToken cancellationToken = default )
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

        return new IncomesResponse()
        {
            Id = income.Id,
            Description = income.Description!,
            Value = income.Value,
            CreationDate = income.CreationDate,
            Source = income.Source!,
            Withholding = income.Withholding
        };
    }
}

internal class GetByIdQueryValidator: QueryValidator<GetByIdQuery>
{
    public GetByIdQueryValidator() : base() { }
}
