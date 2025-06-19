using Application.Abstractions;
using Domain.Payments;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Payments;

public interface IQueryOneHandler: IQueryHandler<GetByIdQuery, PaymentsResponse> { }

public sealed class GetByIdQueryHandler(
    IAppDbContext dbContext )
    : IQueryOneHandler
{
    public async Task<PaymentsResponse> Execute(
        GetByIdQuery query, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( query );

        var validator = new GetByIdQueryValidator();
        var result = await validator.ValidateAsync( query, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var payment = await dbContext.Payments
            .Where( item => item.Id == query.Id )
            .Select( item => new PaymentsResponse()
            {
                Id = item.Id,
                Description = item.Description,
                Value = item.Value,
                CreationDate = item.CreationDate,
                Currency = item.Currency,
                IsAutoDebit = item.IsAutoDebit,
                PaymentMediaReference = item.PaymentMediaReference
            } )
            .FirstOrDefaultAsync( cancellationToken ) ??
                throw new NotFoundPaymentException( query.Id );

        return payment;
    }
}

internal class GetByIdQueryValidator: QueryValidator<GetByIdQuery>
{
    public GetByIdQueryValidator() : base() { }
}
