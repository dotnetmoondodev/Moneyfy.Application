using Microsoft.Extensions.DependencyInjection;

namespace Application.Incomes;

public static class IncomeExtesions
{
    public static IServiceCollection AddIncomeServices( this IServiceCollection services )
    {
        services.AddScoped<ICreateCommandHandler, CreateCommandHandler>();
        services.AddScoped<IUpdateCommandHandler, UpdateCommandHandler>();
        services.AddScoped<IDeleteCommandHandler, DeleteCommandHandler>();
        services.AddScoped<IQueryOneHandler, GetByIdQueryHandler>();
        services.AddScoped<IQueryAllHandler, GetAllQueryHandler>();

        return services;
    }
}

