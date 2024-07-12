using Microsoft.EntityFrameworkCore;
using RateIdeas.Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;
using RateIdeas.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using RateIdeas.Application.Commons.Interfaces;

namespace RateIdeas.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add database
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(name: "DefaultConnection")));


        // Add repositories
        services.AddScoped(serviceType: typeof(IRepository<>), implementationType: typeof(Repository<>));

        return services;
    }
}