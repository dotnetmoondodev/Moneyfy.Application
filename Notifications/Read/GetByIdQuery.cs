using Application.Abstractions;
using Domain.Primitives;

namespace Application.Notifications;

public sealed record GetByIdQuery: BaseQuery { }

public sealed record NotificationsResponse
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset DateToSend { get; set; }
    public int HourToSend { get; set; }
    public NotificationFrequency Frequency { get; set; }
    public NotificationMethod Method { get; set; }
    public Guid? PaymentId { get; set; }
    public bool Repeatable { get; set; }
    public bool Enable { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
