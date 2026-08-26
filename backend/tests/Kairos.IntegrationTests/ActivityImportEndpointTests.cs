using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using Kairos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kairos.IntegrationTests;

public sealed class ActivityImportEndpointTests
{
    [Fact]
    public async Task Authenticated_athlete_can_upload_import_and_read_complete_activity()
    {
        await using var factory = CreateFactory();
        using var client = CreateClient(factory, "athlete-123");
        using var upload = Upload("minimal-cycling.fit", "commute.fit");

        var uploadResponse = await client.PostAsync("/api/activity-imports/fit", upload);
        var uploadBody = JsonDocument.Parse(await uploadResponse.Content.ReadAsStringAsync());
        var uploadId = uploadBody.RootElement.GetProperty("id").GetGuid();
        var importResponse = await client.PostAsync(
            $"/api/activity-imports/fit/{uploadId}/import",
            null
        );
        var importBody = JsonDocument.Parse(await importResponse.Content.ReadAsStringAsync());
        var activityResponse = await client.GetAsync($"/api/activities/{uploadId}");
        var activityBody = JsonDocument.Parse(
            await activityResponse.Content.ReadAsStringAsync()
        );

        Assert.Equal(HttpStatusCode.Created, uploadResponse.StatusCode);
        Assert.Equal(HttpStatusCode.Created, importResponse.StatusCode);
        Assert.Equal("imported", importBody.RootElement.GetProperty("status").GetString());
        Assert.Equal(2, importBody.RootElement.GetProperty("sampleCount").GetInt32());
        Assert.Equal(HttpStatusCode.OK, activityResponse.StatusCode);
        Assert.Equal("cycling", activityBody.RootElement.GetProperty("type").GetString());
        Assert.Equal(
            "commute.fit",
            activityBody.RootElement.GetProperty("source").GetProperty("originalFileName").GetString()
        );
        Assert.Equal(2, activityBody.RootElement.GetProperty("samples").GetArrayLength());
        Assert.Contains(
            activityBody.RootElement.GetProperty("summary").EnumerateArray(),
            metric =>
                metric.GetProperty("code").GetString() == "distance"
                && metric.GetProperty("value").GetDecimal() == 1000m
        );
    }

    [Fact]
    public async Task Stored_activity_is_not_visible_to_another_athlete()
    {
        await using var factory = CreateFactory();
        using var ownerClient = CreateClient(factory, "athlete-123");
        using var upload = Upload("minimal-cycling.fit");
        var uploadResponse = await ownerClient.PostAsync("/api/activity-imports/fit", upload);
        var body = JsonDocument.Parse(await uploadResponse.Content.ReadAsStringAsync());
        var id = body.RootElement.GetProperty("id").GetGuid();
        await ownerClient.PostAsync($"/api/activity-imports/fit/{id}/import", null);

        using var otherClient = CreateClient(factory, "other-athlete");
        var response = await otherClient.GetAsync($"/api/activities/{id}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private static WebApplicationFactory<Program> CreateFactory()
    {
        var databaseName = $"activity-api-{Guid.NewGuid():N}";
        return new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<KairosDbContext>();
                services.RemoveAll<DbContextOptions<KairosDbContext>>();
                services.RemoveAll<IDbContextOptionsConfiguration<KairosDbContext>>();
                services.AddDbContext<KairosDbContext>(options =>
                    options.UseInMemoryDatabase(databaseName)
                );
                services
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = TestAuthenticationHandler.SchemeName;
                        options.DefaultChallengeScheme = TestAuthenticationHandler.SchemeName;
                    })
                    .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                        TestAuthenticationHandler.SchemeName,
                        _ => { }
                    );
            });
        });
    }

    private static HttpClient CreateClient(
        WebApplicationFactory<Program> factory,
        string subject
    )
    {
        var client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "test");
        client.DefaultRequestHeaders.Add(TestAuthenticationHandler.SubjectHeader, subject);
        return client;
    }

    private static MultipartFormDataContent Upload(
        string fixtureName,
        string? uploadName = null
    )
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Fixtures", "Fit", fixtureName);
        var file = new ByteArrayContent(System.IO.File.ReadAllBytes(path));
        file.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        var form = new MultipartFormDataContent();
        form.Add(file, "file", uploadName ?? fixtureName);
        return form;
    }

    private sealed class TestAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder
    ) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        public const string SchemeName = "ActivityImportTest";
        public const string SubjectHeader = "X-Test-Subject";

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (
                Request.Headers.Authorization != "Bearer test"
                || !Request.Headers.TryGetValue(SubjectHeader, out var subject)
            )
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var identity = new ClaimsIdentity(
                [new Claim("sub", subject.ToString())],
                SchemeName
            );
            return Task.FromResult(
                AuthenticateResult.Success(
                    new AuthenticationTicket(new ClaimsPrincipal(identity), SchemeName)
                )
            );
        }
    }
}
