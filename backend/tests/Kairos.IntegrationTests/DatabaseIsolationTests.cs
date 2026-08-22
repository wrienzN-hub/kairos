using Kairos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kairos.IntegrationTests;

public sealed class DatabaseIsolationTests
{
    [Fact]
    public async Task Test_database_is_isolated_and_can_be_created()
    {
        var databaseName = $"kairos-test-{Guid.NewGuid():N}";
        var options = new DbContextOptionsBuilder<KairosDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        await using var context = new KairosDbContext(options);

        Assert.True(await context.Database.EnsureCreatedAsync());
        Assert.Equal("Microsoft.EntityFrameworkCore.InMemory", context.Database.ProviderName);
    }
}
