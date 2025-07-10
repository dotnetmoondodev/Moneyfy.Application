using Application.Abstractions;
using Domain.Payments;
using Domain;

namespace Application.Payments;

public interface IQueryAllHandler: IQueryHandler<IReadOnlyCollection<PaymentsResponse>> { }

internal sealed class GetAllQueryHandler(
    IRepository<Payment> repository )
    : IQueryAllHandler
{
    public async Task<IReadOnlyCollection<PaymentsResponse>> Execute(
        CancellationToken cancellationToken = default )
    {
        var records = await repository.GetAllAsync( null, cancellationToken );
        return [.. records.Select( item => new PaymentsResponse()
        {
            Id = item.Id,
            Description = item.Description!,
            Value = item.Value,
            CreationDate = item.CreationDate,
            Currency = item.Currency,
            IsAutoDebit = item.IsAutoDebit,
            PaymentMediaReference = item.PaymentMediaReference!
        } )];
    }
}
