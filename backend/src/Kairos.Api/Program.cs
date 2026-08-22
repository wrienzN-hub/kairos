using Kairos.Api.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOptions<KairosOptions>()
    .Bind(builder.Configuration.GetRequiredSection(KairosOptions.SectionName))
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.ProductName),
        $"{KairosOptions.SectionName}:ProductName must not be empty.")
    .ValidateOnStart();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapGet(
    "/",
    (IOptions<KairosOptions> options) => Results.Ok(new
    {
        service = options.Value.ProductName,
        status = "running",
    }));

app.MapHealthChecks("/health", new HealthCheckOptions
{
    AllowCachingResponses = false,
});

app.Run();

public partial class Program;
