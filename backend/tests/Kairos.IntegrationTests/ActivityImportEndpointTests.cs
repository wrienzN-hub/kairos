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
        Assert.Equal("limited", importBody.RootElement.GetProperty("analysisStatus").GetString());
        Assert.Equal(4, importBody.RootElement.GetProperty("qualityFindingCount").GetInt32());
        Assert.Equal(HttpStatusCode.OK, activityResponse.StatusCode);
        Assert.Equal("cycling", activityBody.RootElement.GetProperty("type").GetString());
        Assert.Equal(
            "commute.fit",
            activityBody.RootElement.GetProperty("source").GetProperty("originalFileName").GetString()
        );
        Assert.Equal(2, activityBody.RootElement.GetProperty("samples").GetArrayLength());
        Assert.True(
            activityBody.RootElement
                .GetProperty("quality")
                .GetProperty("isAnalysisRestricted")
                .GetBoolean()
        );
        Assert.Contains(
            activityBody.RootElement
                .GetProperty("quality")
                .GetProperty("findings")
                .EnumerateArray(),
            finding => finding.GetProperty("code").GetString() == "missing_power_stream"
        );
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

    [Fact]
    public async Task Repeated_file_import_returns_existing_activity_as_duplicate()
    {
        await using var factory = CreateFactory();
        using var client = CreateClient(factory, "athlete-123");
        using var firstUpload = Upload("minimal-cycling.fit", "first.fit");
        using var secondUpload = Upload("minimal-cycling.fit", "second.fit");
        var firstUploadResponse = await client.PostAsync(
            "/api/activity-imports/fit",
            firstUpload
        );
        var secondUploadResponse = await client.PostAsync(
            "/api/activity-imports/fit",
            secondUpload
        );
        var firstUploadId = JsonDocument
            .Parse(await firstUploadResponse.Content.ReadAsStringAsync())
            .RootElement.GetProperty("id")
            .GetGuid();
        var secondUploadId = JsonDocument
            .Parse(await secondUploadResponse.Content.ReadAsStringAsync())
            .RootElement.GetProperty("id")
            .GetGuid();

        var firstImport = await client.PostAsync(
            $"/api/activity-imports/fit/{firstUploadId}/import",
            null
        );
        var duplicateImport = await client.PostAsync(
            $"/api/activity-imports/fit/{secondUploadId}/import",
            null
        );
        var duplicateBody = JsonDocument.Parse(
            await duplicateImport.Content.ReadAsStringAsync()
        );

        Assert.Equal(HttpStatusCode.Created, firstImport.StatusCode);
        Assert.Equal(HttpStatusCode.OK, duplicateImport.StatusCode);
        Assert.Equal("duplicate", duplicateBody.RootElement.GetProperty("status").GetString());
        Assert.Equal(firstUploadId, duplicateBody.RootElement.GetProperty("id").GetGuid());
        using var scope = factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<KairosDbContext>();
        Assert.Equal(1, await context.Activities.CountAsync());
        Assert.Equal(
            "duplicate",
            (await context.FitUploads.SingleAsync(upload => upload.Id == secondUploadId)).Status
        );
    }

    [Fact]
    public async Task Activity_overview_is_ordered_and_owner_scoped()
    {
        await using var factory = CreateFactory();
        using var ownerClient = CreateClient(factory, "athlete-123");
        using var olderUpload = Upload("valid-cycling.fit");
        using var newerUpload = Upload("incomplete-cycling.fit");
        var olderId = await UploadAndImport(ownerClient, olderUpload);
        var newerId = await UploadAndImport(ownerClient, newerUpload);

        var response = await ownerClient.GetAsync("/api/activities");
        var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, body.RootElement.GetArrayLength());
        Assert.Equal(newerId, body.RootElement[0].GetProperty("id").GetGuid());
        Assert.Equal("limited", body.RootElement[0].GetProperty("analysisStatus").GetString());
        Assert.Equal(4500m, body.RootElement[0].GetProperty("distanceMeters").GetDecimal());
        Assert.Equal(olderId, body.RootElement[1].GetProperty("id").GetGuid());

        using var otherClient = CreateClient(factory, "other-athlete");
        var otherResponse = await otherClient.GetAsync("/api/activities");
        var otherBody = JsonDocument.Parse(await otherResponse.Content.ReadAsStringAsync());
        Assert.Empty(otherBody.RootElement.EnumerateArray());
    }

    [Fact]
    public async Task Owner_can_export_normalized_activity_with_provenance_and_audit()
    {
        await using var factory = CreateFactory();
        using var client = CreateClient(factory, "athlete-123");
        using var upload = Upload("minimal-cycling.fit", "export-me.fit");
        var id = await UploadAndImport(client, upload);

        var response = await client.GetAsync($"/api/activities/{id}/export");
        var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
        Assert.Contains($"kairos-activity-{id}.json", response.Content.Headers.ContentDisposition?.FileName);
        Assert.Equal(1, body.RootElement.GetProperty("schemaVersion").GetInt32());
        var activity = body.RootElement.GetProperty("activity");
        Assert.Equal(id, activity.GetProperty("id").GetGuid());
        Assert.Equal(
            64,
            activity.GetProperty("source").GetProperty("contentHashSha256").GetString()?.Length
        );
        Assert.Contains(
            activity.GetProperty("summary").EnumerateArray(),
            metric => metric.GetProperty("provenance").GetProperty("sourceField").ValueKind
                == JsonValueKind.String
        );

        using var scope = factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<KairosDbContext>();
        var audit = await context.ActivityAuditEvents.SingleAsync();
        Assert.Equal("exported", audit.Action);
        Assert.Equal(id, audit.ActivityId);
        Assert.Equal("athlete-123", audit.OwnerSubject);
    }

    [Fact]
    public async Task Delete_removes_activity_and_raw_upload_but_preserves_audit()
    {
        await using var factory = CreateFactory();
        using var ownerClient = CreateClient(factory, "athlete-123");
        using var upload = Upload("interval-cycling.fit", "delete-me.fit");
        var id = await UploadAndImport(ownerClient, upload);

        using var otherClient = CreateClient(factory, "other-athlete");
        Assert.Equal(
            HttpStatusCode.NotFound,
            (await otherClient.GetAsync($"/api/activities/{id}/export")).StatusCode
        );
        Assert.Equal(
            HttpStatusCode.NotFound,
            (await otherClient.DeleteAsync($"/api/activities/{id}")).StatusCode
        );

        var response = await ownerClient.DeleteAsync($"/api/activities/{id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(
            HttpStatusCode.NotFound,
            (await ownerClient.GetAsync($"/api/activities/{id}")).StatusCode
        );
        using var scope = factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<KairosDbContext>();
        Assert.Empty(await context.Activities.ToArrayAsync());
        Assert.Empty(await context.FitUploads.ToArrayAsync());
        var audit = await context.ActivityAuditEvents.SingleAsync();
        Assert.Equal("deleted", audit.Action);
        Assert.Equal("athlete-123", audit.OwnerSubject);
        Assert.Contains("\"SampleCount\":5", audit.Details);
        Assert.Contains("\"RawUploadDeleted\":true", audit.Details);
    }

    private static async Task<Guid> UploadAndImport(
        HttpClient client,
        MultipartFormDataContent upload
    )
    {
        var response = await client.PostAsync("/api/activity-imports/fit", upload);
        var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var id = body.RootElement.GetProperty("id").GetGuid();
        var import = await client.PostAsync($"/api/activity-imports/fit/{id}/import", null);
        Assert.Equal(HttpStatusCode.Created, import.StatusCode);
        return id;
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
