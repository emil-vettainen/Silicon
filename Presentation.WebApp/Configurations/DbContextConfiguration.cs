using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Presentation.WebApp.Configurations;

public static class DbContextConfiguration
{
    public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AccountDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("SQLServer")));
        
    }
}