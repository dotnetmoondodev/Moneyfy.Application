using Application.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Application.Expenses;

public sealed record UpdateCommand: BaseCommand
{
    [Required( ErrorMessage = "Id is required." )]
    public Guid Id { get; init; }
}
