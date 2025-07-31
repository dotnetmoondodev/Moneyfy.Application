namespace Application.Expenses;

using Microsoft.Extensions.DependencyInjection;

public static class ExpenseExtensions
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
