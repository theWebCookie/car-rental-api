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
}