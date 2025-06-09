using Microsoft.Extensions.DependencyInjection;

namespace Application.Notifications;

public static class NotificationExtesions
{
    public static IServiceCollection AddNotificationServices( this IServiceCollection services )
    {
        services.AddScoped<ICreateCommandHandler, CreateCommandHandler>();
        services.AddScoped<IUpdateCommandHandler, UpdateCommandHandler>();
        services.AddScoped<IDeleteCommandHandler, DeleteCommandHandler>();
        services.AddScoped<IQueryOneHandler, GetByIdQueryHandler>();
        services.AddScoped<IQueryAllHandler, GetAllQueryHandler>();

        return services;
    }
}

