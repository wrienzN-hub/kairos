using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;

namespace Kairos.IntegrationTests;

public sealed class ConfigurationValidationTests
{
    [Fact]
    public void Missing_product_name_fails_when_the_application_starts()
    {
        using var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting("Kairos:ProductName", string.Empty);
            });

        Assert.Throws<OptionsValidationException>(() => factory.CreateClient());
    }
}
