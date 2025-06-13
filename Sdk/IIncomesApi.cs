using Application.Incomes;
using Domain.Incomes;
using Refit;

namespace Application.Sdk;

[Headers( "Authorization: Bearer" )]
public interface IIncomesApi
{
    [Get( ApiEndpoints.Incomes.GetOne )]
    Task<Income> GetIncomeAsync( Guid id );

    [Get( ApiEndpoints.Incomes.GetAll )]
    Task<IEnumerable<Income>> GetIncomesAsync();

    [Post( ApiEndpoints.Incomes.Create )]
    Task<Income> CreateIncomeAsync( CreateCommand request );

    [Put( ApiEndpoints.Incomes.Update )]
    Task<Income> UpdateIncomeAsync( UpdateCommand request );

    [Delete( ApiEndpoints.Incomes.Delete )]
    Task DeleteIncomeAsync( Guid id );
}

