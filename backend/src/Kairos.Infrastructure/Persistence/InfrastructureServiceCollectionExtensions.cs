using Kairos.Application.ActivityImports;
using Kairos.Application.Activities;
using Kairos.Infrastructure.ActivityImports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kairos.Infrastructure.Persistence;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddKairosInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddDbContext<KairosDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IFitUploadStore, EfFitUploadStore>();
        services.AddScoped<IActivityStore, EfActivityStore>();
        services.AddSingleton<IFitActivityParser, GarminFitActivityParser>();

        return services;
    }
}
