using Application.Abstractions;
using Domain.Payments;
using FluentValidation;

namespace Application.Payments;

public interface IQueryOneHandler: IQueryHandler<GetByIdQuery, Payment> { }

public sealed class GetByIdQueryHandler(
    IPaymentsRepository repository )
    : IQueryOneHandler
{
    public async Task<Payment> Execute( GetByIdQuery query, CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( query );

        var validator = new GetByIdQueryValidator();
        var result = await validator.ValidateAsync( query, cancellationToken );

        if ( !result.IsValid )
        {
            throw new ValidationException( result.Errors );
        }

        var payment = await repository.GetByIdAsync( query.Id, cancellationToken ) ??
            throw new NotFoundPaymentException( query.Id );

        return payment;
    }
}

internal class GetByIdQueryValidator: QueryValidator<GetByIdQuery>
{
    public GetByIdQueryValidator() : base() { }
}
