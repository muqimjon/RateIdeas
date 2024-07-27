using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RateIdeas.Application.Commons.Interfaces;
using RateIdeas.Application.Senders.EmailServices.Models;
using RateIdeas.Infrastructure.Contexts;
using RateIdeas.Infrastructure.Repositories;

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
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.Configure<EmailConfigurations>(configuration.GetSection("EmailConfigurations"));

        return services;
    }
}