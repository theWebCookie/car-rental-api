using CarRent.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Api.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CarRentContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("CrContext");
        services.AddSqlServer<CarRentContext>(connString).AddScoped<ICarsRepository, EntityFrameworkCarsRepository>();
        return services;
    }
}