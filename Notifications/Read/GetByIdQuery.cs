using System.ComponentModel.DataAnnotations;
using Application.Abstractions;
using Domain.Notifications;
using Domain.Primitives;

namespace Application.Notifications;

public sealed record GetByIdQuery: BaseQuery { }

public sealed record NotificationsResponse
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }

    [Required( ErrorMessage = "Description is required." )]
    [StringLength( 128, ErrorMessage = "Description cannot exceed 128 characters." )]
    public required string Description { get; set; }

    [Required( ErrorMessage = "Date to send is required." )]
    [DataType( DataType.DateTime, ErrorMessage = "Date to send must be a valid Date." )]
    public required DateTime DateToSend { get; set; }

    [Required( ErrorMessage = "Hour to send is required." )]
    [EnumDataType( typeof( CustomHours ), ErrorMessage = "Hour to send must be a valid CustomHours value." )]
    public required CustomHours HourToSend { get; set; }

    [Required( ErrorMessage = "Frequency is required." )]
    [EnumDataType( typeof( NotificationFrequency ), ErrorMessage = "Frequency must be a valid NotificationFrequency value." )]
    public required NotificationFrequency Frequency { get; set; }

    [Required( ErrorMessage = "Method is required." )]
    [EnumDataType( typeof( NotificationMethod ), ErrorMessage = "Method must be a valid NotificationMethod value." )]
    public required NotificationMethod Method { get; set; }

    [Required( ErrorMessage = "Payment is required." )]
    public required Guid? PaymentId { get; set; }

    [Required( ErrorMessage = "Repeatable is required." )]
    [EnumDataType( typeof( CustomAnswer ), ErrorMessage = "Invalid answer type." )]
    public required CustomAnswer Repeatable { get; set; }

    [Required( ErrorMessage = "Enable is required." )]
    [EnumDataType( typeof( CustomAnswer ), ErrorMessage = "Invalid answer type." )]
    public required CustomAnswer Enable { get; set; }

    [EmailRequired( nameof( Method ), NotificationMethod.Email )]
    public string? Email { get; set; }

    [PhoneRequired( nameof( Method ), [NotificationMethod.Sms, NotificationMethod.Push] )]
    public string? PhoneNumber { get; set; }
}