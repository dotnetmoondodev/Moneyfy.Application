using Domain.Primitives;
using System.ComponentModel.DataAnnotations;

namespace Application.Notifications;

public sealed record UpdateCommand
{
    [Required( ErrorMessage = "Id is required." )]
    public Guid Id { get; init; }

    [Required( ErrorMessage = "Description is required." )]
    [StringLength( 128, ErrorMessage = "Description cannot exceed 128 characters." )]
    public required string Description { get; init; }

    [Required( ErrorMessage = "DateToSend is required." )]
    [DataType( DataType.DateTime, ErrorMessage = "DateToSend must be a valid DateTime." )]
    public required DateTime DateToSend { get; init; }

    [Required( ErrorMessage = "HourToSend is required." )]
    [EnumDataType( typeof( CustomHours ), ErrorMessage = "HourToSend must be a valid CustomHours value." )]
    public required CustomHours HourToSend { get; init; }

    [Required( ErrorMessage = "Frequency is required." )]
    [EnumDataType( typeof( NotificationFrequency ), ErrorMessage = "Frequency must be a valid NotificationFrequency value." )]
    public required NotificationFrequency Frequency { get; init; }

    [Required( ErrorMessage = "Method is required." )]
    [EnumDataType( typeof( NotificationMethod ), ErrorMessage = "Method must be a valid NotificationMethod value." )]
    public required NotificationMethod Method { get; init; }

    public Guid? PaymentId { get; init; }

    [Required( ErrorMessage = "Repeatable is required." )]
    [EnumDataType( typeof( CustomAnswer ), ErrorMessage = "Invalid answer type." )]
    public required CustomAnswer Repeatable { get; init; }

    [Required( ErrorMessage = "Enable is required." )]
    [EnumDataType( typeof( CustomAnswer ), ErrorMessage = "Invalid answer type." )]
    public required CustomAnswer Enable { get; init; }

    [EmailAddress( ErrorMessage = "Email must be a valid email address." )]
    public string? Email { get; init; }

    [Phone( ErrorMessage = "PhoneNumber must be a valid phone number." )]
    public string? PhoneNumber { get; init; }
}
