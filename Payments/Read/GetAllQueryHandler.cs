using Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Application.Payments;

public interface IQueryAllHandler: IQueryHandler<IEnumerable<PaymentsResponse>> { }

internal sealed class GetAllQueryHandler(
    IAppDbContext dbContext )
    : IQueryAllHandler
{
    public async Task<IEnumerable<PaymentsResponse>> Execute(
        CancellationToken cancellationToken = default )
    {
        return await dbContext.Payments
            .Select( item => new PaymentsResponse()
            {
                Id = item.Id,
                Description = item.Description!,
                Value = item.Value,
                CreationDate = item.CreationDate,
                Currency = item.Currency,
                IsAutoDebit = item.IsAutoDebit,
                PaymentMediaReference = item.PaymentMediaReference!
            } )
            .ToListAsync( cancellationToken );
    }
}
