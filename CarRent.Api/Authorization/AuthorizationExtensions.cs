namespace CarRent.Api.Authorization;

public static class AuthorizationExtensions
{
  public static IServiceCollection AddCarStoreAuthorization(this IServiceCollection services)
  {
    services.AddAuthorization(options =>
    {
      options.AddPolicy(Policies.ReadAccess, builder => builder.RequireClaim("scope", "read"));
      options.AddPolicy(Policies.WriteAccess, builder => builder.RequireClaim("scope", "write")
                                                                .RequireRole("Admin"));
    });
    return services;
  }
}