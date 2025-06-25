using Domain.Primitives;

namespace Application.Notifications;

public sealed record UpdateCommand
{
    public Guid Id { get; init; }
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
