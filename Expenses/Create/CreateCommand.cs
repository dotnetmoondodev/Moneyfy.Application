using Application.Abstractions;
using Domain.Expenses;

namespace Application.Expenses;

public sealed record CreateCommand: BaseCommand { }

internal static partial class CreateCommandExtensions
{
    public static Expense MapToExpense( this CreateCommand command )
    {
        return command is null ?
            throw new ArgumentNullException( nameof( command ) ) :
            new Expense(
                command.Description,
                command.Value );
    }
}
