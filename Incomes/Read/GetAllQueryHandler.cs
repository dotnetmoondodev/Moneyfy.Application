using Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Application.Incomes;

public interface IQueryAllHandler: IQueryHandler<IEnumerable<IncomesResponse>> { }

internal sealed class GetAllQueryHandler(
    IAppDbContext dbContext )
    : IQueryAllHandler
{
    public async Task<IEnumerable<IncomesResponse>> Execute(
        CancellationToken cancellationToken = default )
    {
        return await dbContext.Incomes
            .Select( item => new IncomesResponse()
            {
                Id = item.Id,
                Description = item.Description!,
                Value = item.Value,
                CreationDate = item.CreationDate,
                Source = item.Source!,
                Withholding = item.Withholding
            } )
            .ToListAsync( cancellationToken );
    }
}
