using Domain.Notifications;
using Domain.Primitives;

namespace Application.Notifications;

public sealed record CreateCommand
{
    public string? Description { get; init; }
    public DateTime DateToSend { get; init; }
    public CustomHours HourToSend { get; init; }
    public NotificationFrequency Frequency { get; init; }
    public NotificationMethod Method { get; init; }
    public Guid? PaymentId { get; init; }
    public CustomAnswer Repeatable { get; init; }
    public CustomAnswer Enable { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
}

internal static partial class CreateCommandExtensions
{
    public static Notification MapToNotification( this CreateCommand command )
    {
        return command is null ?
            throw new ArgumentNullException( nameof( command ) ) :
            new Notification(
                command.Description!,
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
