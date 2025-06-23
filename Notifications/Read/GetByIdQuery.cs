using Application.Abstractions;
using Domain.Primitives;

namespace Application.Notifications;

public sealed record GetByIdQuery: BaseQuery { }

public sealed record NotificationsResponse
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime DateToSend { get; set; }
    public CustomHours HourToSend { get; set; }
    public NotificationFrequency Frequency { get; set; }
    public NotificationMethod Method { get; set; }
    public Guid? PaymentId { get; set; }
    public CustomAnswer Repeatable { get; set; }
    public CustomAnswer Enable { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
