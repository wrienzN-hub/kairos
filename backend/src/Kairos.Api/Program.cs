using Kairos.Api.ActivityImports;
using Kairos.Api.Activities;
using Kairos.Api.Authentication;
using Kairos.Api.Configuration;
using Kairos.Application.ActivityImports;
using Kairos.Application.Activities;
using Kairos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOptions<KairosOptions>()
    .Bind(builder.Configuration.GetRequiredSection(KairosOptions.SectionName))
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.ProductName),
        $"{KairosOptions.SectionName}:ProductName must not be empty.")
    .ValidateOnStart();

builder.Services
    .AddOptions<FitUploadOptions>()
    .Bind(builder.Configuration.GetRequiredSection(FitUploadOptions.SectionName))
    .Validate(
        options => options.MaximumFileSizeBytes is > 0 and <= int.MaxValue,
        $"{FitUploadOptions.SectionName}:MaximumFileSizeBytes must be between 1 and {int.MaxValue}."
    )
    .ValidateOnStart();

var fitUploadOptions = builder.Configuration
    .GetRequiredSection(FitUploadOptions.SectionName)
    .Get<FitUploadOptions>()
    ?? throw new InvalidOperationException(
        $"{FitUploadOptions.SectionName} configuration is required."
    );
var multipartBodyLengthLimit = checked(
    fitUploadOptions.MaximumFileSizeBytes + 1024 * 1024
);
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = multipartBodyLengthLimit;
});
builder.Services.AddSingleton(new FitUploadPolicy(fitUploadOptions.MaximumFileSizeBytes));
builder.Services.AddScoped<FitUploadService>();
builder.Services.AddScoped<FitActivityImportService>();
builder.Services.AddSingleton<ActivityQualityEvaluator>();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = multipartBodyLengthLimit;
});

var authentication = builder.Configuration
    .GetRequiredSection(AuthenticationOptions.SectionName)
    .Get<AuthenticationOptions>()
    ?? throw new InvalidOperationException(
        $"{AuthenticationOptions.SectionName} configuration is required.");

if (string.IsNullOrWhiteSpace(authentication.Authority)
    || string.IsNullOrWhiteSpace(authentication.Audience))
{
    throw new InvalidOperationException(
        "Authentication:Authority and Authentication:Audience must be configured.");
}

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = authentication.Authority;
        options.Audience = authentication.Audience;
        options.RequireHttpsMetadata = authentication.RequireHttpsMetadata;
        options.MapInboundClaims = false;
        if (!string.IsNullOrWhiteSpace(authentication.BackchannelAuthority))
        {
            options.BackchannelHttpHandler = new AuthorityRewriteHandler(
                new Uri(authentication.Authority),
                new Uri(authentication.BackchannelAuthority));
        }
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name",
            RoleClaimType = "roles",
            ValidIssuer = string.IsNullOrWhiteSpace(authentication.ValidIssuer)
                ? authentication.Authority
                : authentication.ValidIssuer,
        };
    });
builder.Services.AddAuthorization();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapGet(
    "/",
    (IOptions<KairosOptions> options) => Results.Ok(new
    {
        service = options.Value.ProductName,
        status = "running",
    }));

app.MapGet(
        "/api/me",
        (ClaimsPrincipal user) => Results.Ok(new
        {
            id = user.FindFirstValue("sub"),
            name = user.FindFirstValue("name"),
            email = user.FindFirstValue("email"),
        }))
    .RequireAuthorization();

app.MapFitUploadEndpoints();
app.MapActivityEndpoints();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    AllowCachingResponses = false,
});

app.Run();

public partial class Program;
