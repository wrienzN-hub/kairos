using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Kairos.Infrastructure.Persistence;

public sealed class KairosDbContextFactory : IDesignTimeDbContextFactory<KairosDbContext>
{
    public KairosDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("KAIROS_CONNECTION_STRING")
            ?? "Host=localhost;Port=5432;Database=kairos;Username=kairos;Password=kairos_dev_only";

        var options = new DbContextOptionsBuilder<KairosDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new KairosDbContext(options);
    }
}
