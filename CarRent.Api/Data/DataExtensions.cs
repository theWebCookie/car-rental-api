using CarRent.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Api.Data;

public static class DataExtensions
{
    public static void InitializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CarRentContext>();
        dbContext.Database.Migrate();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("CrContext");
        services.AddSqlServer<CarRentContext>(connString).AddScoped<ICarsRepository, EntityFrameworkCarsRepository>();
        return services;
    }
}