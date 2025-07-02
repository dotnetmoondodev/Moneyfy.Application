using Application.Abstractions;
using Domain.Incomes;

namespace Application.Incomes;

public interface IQueryAllHandler: IQueryHandler<IReadOnlyCollection<IncomesResponse>> { }

internal sealed class GetAllQueryHandler(
    IIncomesRepository repository )
    : IQueryAllHandler
{
    public async Task<IReadOnlyCollection<IncomesResponse>> Execute(
        CancellationToken cancellationToken = default )
    {
        var records = await repository.GetAllAsync( null, cancellationToken );
        return [.. records.Select( item => new IncomesResponse()
        {
            Id = item.Id,
            Description = item.Description!,
            Value = item.Value,
            CreationDate = item.CreationDate,
            Source = item.Source!,
            Withholding = item.Withholding
        } )];
    }
}
