using Application.Payments;
using Domain.Payments;
using Refit;

namespace Application.Sdk;

[Headers( "Authorization: Bearer" )]
public interface IPaymentsApi
{
    [Get( ApiEndpoints.Payments.GetOne )]
    Task<Payment> GetPaymentAsync( Guid id );

    [Get( ApiEndpoints.Payments.GetAll )]
    Task<IEnumerable<Payment>> GetPaymentsAsync();

    [Post( ApiEndpoints.Payments.Create )]
    Task<Payment> CreatePaymentAsync( CreateCommand request );

    [Put( ApiEndpoints.Payments.Update )]
    Task<Payment> UpdatePaymentAsync( UpdateCommand request );

    [Delete( ApiEndpoints.Payments.Delete )]
    Task DeletePaymentAsync( Guid id );
}

