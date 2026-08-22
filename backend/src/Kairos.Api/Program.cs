using Kairos.Api.Configuration;
using Kairos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOptions<KairosOptions>()
    .Bind(builder.Configuration.GetRequiredSection(KairosOptions.SectionName))
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.ProductName),
        $"{KairosOptions.SectionName}:ProductName must not be empty.")
    .ValidateOnStart();

var connectionString = builder.Configuration.GetConnectionString("Kairos")
    ?? throw new InvalidOperationException(
        "ConnectionStrings:Kairos must be configured.");

builder.Services.AddKairosInfrastructure(connectionString);
builder.Services.AddHealthChecks();

var app = builder.Build();

if (builder.Configuration.GetValue<bool>("Database:ApplyMigrationsOnStartup"))
{
    await using var scope = app.Services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<KairosDbContext>();
    await dbContext.Database.MigrateAsync();
}

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
