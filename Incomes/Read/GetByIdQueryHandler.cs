using Application.Abstractions;
using Domain.Incomes;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Incomes;

public interface IQueryOneHandler: IQueryHandler<GetByIdQuery, IncomesResponse> { }

public sealed class GetByIdQueryHandler(
    IAppDbContext dbContext )
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

        var income = await dbContext.Incomes
            .Where( item => item.Id == query.Id )
            .Select( item => new IncomesResponse()
            {
                Id = item.Id,
                Description = item.Description!,
                Value = item.Value,
                CreationDate = item.CreationDate,
                Source = item.Source!,
                Withholding = item.Withholding
            } )
            .FirstOrDefaultAsync( cancellationToken ) ??
                throw new NotFoundIncomeException( query.Id );

        return income;
    }
}

internal class GetByIdQueryValidator: QueryValidator<GetByIdQuery>
{
    public GetByIdQueryValidator() : base() { }
}
