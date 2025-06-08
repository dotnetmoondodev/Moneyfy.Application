using System.ComponentModel.DataAnnotations;

namespace Application.Abstractions;

public abstract record BaseQuery
{
    [Required( ErrorMessage = "Id is required." )]
    public required Guid Id { get; init; }
}
