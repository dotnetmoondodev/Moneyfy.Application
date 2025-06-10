using Microsoft.Extensions.DependencyInjection;

namespace Application.Payments;

public static class PaymentExtesions
{
    public static IServiceCollection AddApplicationServices( this IServiceCollection services )
    {
        services.AddScoped<ICreateCommandHandler, CreateCommandHandler>();
        services.AddScoped<IUpdateCommandHandler, UpdateCommandHandler>();
        services.AddScoped<IDeleteCommandHandler, DeleteCommandHandler>();
        services.AddScoped<IQueryOneHandler, GetByIdQueryHandler>();
        services.AddScoped<IQueryAllHandler, GetAllQueryHandler>();

        return services;
    }
}

