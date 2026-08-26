using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using Kairos.Application.ActivityImports;
using Kairos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kairos.IntegrationTests;

public sealed class FitUploadEndpointTests
{
    private static readonly string FixtureDirectory = Path.Combine(
        AppContext.BaseDirectory,
        "Fixtures",
        "Fit"
    );

    [Fact]
    public async Task Anonymous_upload_is_rejected()
    {
        await using var factory = CreateFactory();
        using var client = factory.CreateClient();
        using var content = CreateUpload("minimal-cycling.fit");

        var response = await client.PostAsync("/api/activity-imports/fit", content);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Valid_fit_is_stored_for_the_authenticated_owner()
    {
        await using var factory = CreateFactory();
        using var client = CreateAuthenticatedClient(factory, "athlete-123");
        using var content = CreateUpload("minimal-cycling.fit", "morning.fit");

        var response = await client.PostAsync("/api/activity-imports/fit", content);
        var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal("pending", body.RootElement.GetProperty("status").GetString());
        Assert.Equal("morning.fit", body.RootElement.GetProperty("originalFileName").GetString());

        var stored = Assert.Single(factory.Services.GetRequiredService<TestFitUploadStore>().Uploads);
        Assert.Equal("athlete-123", stored.OwnerSubject);
        Assert.Equal("morning.fit", stored.OriginalFileName);
        Assert.Equal(stored.Content.LongLength, stored.SizeBytes);
        Assert.DoesNotContain("minimal-cycling", stored.Sha256);
    }

    [Fact]
    public async Task Corrupted_fit_returns_an_actionable_error_without_persisting_data()
    {
        await using var factory = CreateFactory();
        using var client = CreateAuthenticatedClient(factory, "athlete-123");
        using var content = CreateUpload("corrupted-crc.fit");

        var response = await client.PostAsync("/api/activity-imports/fit", content);
        var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("invalid_fit_crc", body.RootElement.GetProperty("code").GetString());

        Assert.Empty(factory.Services.GetRequiredService<TestFitUploadStore>().Uploads);
    }

    [Fact]
    public async Task Upload_metadata_is_visible_only_to_its_owner()
    {
        await using var factory = CreateFactory();
        using var ownerClient = CreateAuthenticatedClient(factory, "athlete-123");
        using var content = CreateUpload("minimal-cycling.fit");
        var uploadResponse = await ownerClient.PostAsync("/api/activity-imports/fit", content);
        var uploadBody = JsonDocument.Parse(await uploadResponse.Content.ReadAsStringAsync());
        var id = uploadBody.RootElement.GetProperty("id").GetGuid();

        var ownerResponse = await ownerClient.GetAsync($"/api/activity-imports/fit/{id}");
        using var otherClient = CreateAuthenticatedClient(factory, "other-athlete");
        var otherResponse = await otherClient.GetAsync($"/api/activity-imports/fit/{id}");

        Assert.Equal(HttpStatusCode.OK, ownerResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, otherResponse.StatusCode);
    }

    [Fact]
    public async Task Unsupported_extension_is_rejected_without_persisting_data()
    {
        await using var factory = CreateFactory();
        using var client = CreateAuthenticatedClient(factory, "athlete-123");
        using var content = CreateUpload("minimal-cycling.fit", "activity.txt", "text/plain");

        var response = await client.PostAsync("/api/activity-imports/fit", content);
        var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        Assert.Equal("unsupported_file_type", body.RootElement.GetProperty("code").GetString());

        Assert.Empty(factory.Services.GetRequiredService<TestFitUploadStore>().Uploads);
    }

    [Fact]
    public async Task Oversized_file_returns_payload_too_large_without_persisting_data()
    {
        await using var factory = CreateFactory(maximumFileSizeBytes: 32);
        using var client = CreateAuthenticatedClient(factory, "athlete-123");
        using var content = CreateUpload("minimal-cycling.fit");

        var response = await client.PostAsync("/api/activity-imports/fit", content);
        var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.RequestEntityTooLarge, response.StatusCode);
        Assert.Equal("file_too_large", body.RootElement.GetProperty("code").GetString());
        Assert.Empty(factory.Services.GetRequiredService<TestFitUploadStore>().Uploads);
    }

    private static WebApplicationFactory<Program> CreateFactory(
        long? maximumFileSizeBytes = null
    )
    {
        return new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<IFitUploadStore>();
                services.AddSingleton<TestFitUploadStore>();
                services.AddSingleton<IFitUploadStore>(provider =>
                    provider.GetRequiredService<TestFitUploadStore>()
                );
                if (maximumFileSizeBytes is not null)
                {
                    services.RemoveAll<FitUploadPolicy>();
                    services.AddSingleton(new FitUploadPolicy(maximumFileSizeBytes.Value));
                }
                services
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = SubjectAuthenticationHandler.SchemeName;
                        options.DefaultChallengeScheme = SubjectAuthenticationHandler.SchemeName;
                    })
                    .AddScheme<AuthenticationSchemeOptions, SubjectAuthenticationHandler>(
                        SubjectAuthenticationHandler.SchemeName,
                        _ => { }
                    );
            });
        });
    }

    private sealed class TestFitUploadStore : IFitUploadStore
    {
        public List<FitUploadSubmission> Uploads { get; } = [];

        public Task AddAsync(FitUploadSubmission upload, CancellationToken cancellationToken)
        {
            Uploads.Add(upload);
            return Task.CompletedTask;
        }

        public Task<FitUploadReceipt?> FindAsync(
            Guid id,
            string ownerSubject,
            CancellationToken cancellationToken
        )
        {
            var upload = Uploads.SingleOrDefault(value =>
                value.Id == id && value.OwnerSubject == ownerSubject
            );
            return Task.FromResult(
                upload is null
                    ? null
                    : new FitUploadReceipt(
                        upload.Id,
                        upload.OriginalFileName,
                        upload.SizeBytes,
                        upload.Sha256,
                        upload.UploadedAtUtc,
                        "pending"
                    )
            );
        }

        public Task<FitUploadContent?> LoadAsync(
            Guid id,
            string ownerSubject,
            CancellationToken cancellationToken
        )
        {
            var upload = Uploads.SingleOrDefault(value =>
                value.Id == id && value.OwnerSubject == ownerSubject
            );
            return Task.FromResult(
                upload is null
                    ? null
                    : new FitUploadContent(
                        upload.Id,
                        upload.OwnerSubject,
                        upload.OriginalFileName,
                        upload.Sha256,
                        upload.UploadedAtUtc,
                        "pending",
                        upload.Content
                    )
            );
        }
    }

    private static HttpClient CreateAuthenticatedClient(
        WebApplicationFactory<Program> factory,
        string subject
    )
    {
        var client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "test");
        client.DefaultRequestHeaders.Add(SubjectAuthenticationHandler.SubjectHeader, subject);
        return client;
    }

    private static MultipartFormDataContent CreateUpload(
        string fixtureName,
        string? uploadName = null,
        string mediaType = "application/octet-stream"
    )
    {
        var file = new ByteArrayContent(
            File.ReadAllBytes(Path.Combine(FixtureDirectory, fixtureName))
        );
        file.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
        var form = new MultipartFormDataContent();
        form.Add(file, "file", uploadName ?? fixtureName);
        return form;
    }

    private sealed class SubjectAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        public const string SchemeName = "FitUploadTest";
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

            var identity = new ClaimsIdentity([new Claim("sub", subject.ToString())], SchemeName);
            var principal = new ClaimsPrincipal(identity);
            return Task.FromResult(
                AuthenticateResult.Success(new AuthenticationTicket(principal, SchemeName))
            );
        }
    }
}
