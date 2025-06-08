using Domain.Notifications;
using Domain.Primitives;
using System.ComponentModel.DataAnnotations;

namespace Application.Notifications;

public sealed record CreateCommand
{
    [Required( ErrorMessage = "Description is required." )]
    [StringLength( 128, ErrorMessage = "Description cannot exceed 128 characters." )]
    public required string Description { get; init; }

    [Required( ErrorMessage = "DateToSend is required." )]
    [DataType( DataType.DateTime, ErrorMessage = "DateToSend must be a valid DateTimeOffset." )]
    public required DateTimeOffset DateToSend { get; init; }

    [Required( ErrorMessage = "HourToSend is required." )]
    [Range( 0, 23, ErrorMessage = "HourToSend must be between 0 and 23." )]
    public required int HourToSend { get; init; }

    [Required( ErrorMessage = "Frequency is required." )]
    [EnumDataType( typeof( NotificationFrequency ), ErrorMessage = "Frequency must be a valid NotificationFrequency value." )]
    public required NotificationFrequency Frequency { get; init; }

    [Required( ErrorMessage = "Method is required." )]
    [EnumDataType( typeof( NotificationMethod ), ErrorMessage = "Method must be a valid NotificationMethod value." )]
    public required NotificationMethod Method { get; init; }

    public Guid? PaymentId { get; init; }

    [Required( ErrorMessage = "Repeatable is required." )]
    [Range( 0, 1, ErrorMessage = "Repeatable must be either true (1) or false (0)." )]
    public required bool Repeatable { get; init; }

    [Required( ErrorMessage = "Enable is required." )]
    [Range( 0, 1, ErrorMessage = "Enable must be either true (1) or false (0)." )]
    public required bool Enable { get; init; }

    [EmailAddress( ErrorMessage = "Email must be a valid email address." )]
    public string? Email { get; init; }

    [Phone( ErrorMessage = "PhoneNumber must be a valid phone number." )]
    public string? PhoneNumber { get; init; }
}

internal static partial class CreateCommandExtensions
{
    public static Notification MapToNotification( this CreateCommand command )
    {
        return command is null ?
            throw new ArgumentNullException( nameof( command ) ) :
            new Notification(
                command.Description,
                command.DateToSend,
                command.HourToSend,
                command.Frequency,
                command.Method,
                command.PaymentId,
                command.Repeatable,
                command.Enable,
                command.Email,
                command.PhoneNumber );
    }
}
