using Infrastructure.Repositories;

namespace Presentation.WebApp.Configurations;

public static class RepositoriesConfiguration
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<UserAddressRepository>();
        services.AddScoped<OptionalAddressRepository>();
        services.AddScoped<AddressRepository>();
        services.AddScoped<SavedCourseRepository>();
    }
}