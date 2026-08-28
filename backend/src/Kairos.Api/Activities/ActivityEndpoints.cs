using System.Security.Claims;
using Kairos.Application.ActivityImports;
using Kairos.Domain.Activities;

namespace Kairos.Api.Activities;

public static class ActivityEndpoints
{
    public static IEndpointRouteBuilder MapActivityEndpoints(
        this IEndpointRouteBuilder endpoints
    )
    {
        endpoints
            .MapPost("/api/activity-imports/fit/{id:guid}/import", ImportAsync)
            .RequireAuthorization();
        endpoints.MapGet("/api/activities/{id:guid}", FindAsync).RequireAuthorization();
        endpoints.MapGet("/api/activities", ListAsync).RequireAuthorization();
        return endpoints;
    }

    private static async Task<IResult> ListAsync(
        ClaimsPrincipal user,
        FitActivityImportService service,
        CancellationToken cancellationToken
    )
    {
        var ownerSubject = user.FindFirstValue("sub");
        if (string.IsNullOrWhiteSpace(ownerSubject))
        {
            return Results.Forbid();
        }

        return Results.Ok(
            await service.ListAsync(ownerSubject, cancellationToken)
        );
    }

    private static async Task<IResult> ImportAsync(
        Guid id,
        ClaimsPrincipal user,
        FitActivityImportService service,
        CancellationToken cancellationToken
    )
    {
        var ownerSubject = user.FindFirstValue("sub");
        if (string.IsNullOrWhiteSpace(ownerSubject))
        {
            return Results.Forbid();
        }

        try
        {
            var receipt = await service.ImportAsync(
                id,
                ownerSubject,
                cancellationToken
            );
            return receipt.Status == "duplicate"
                ? Results.Ok(receipt)
                : Results.Created($"/api/activities/{receipt.Id}", receipt);
        }
        catch (ActivityImportException exception)
        {
            var statusCode = exception.Code switch
            {
                "fit_upload_not_found" => StatusCodes.Status404NotFound,
                "fit_upload_not_pending" => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status422UnprocessableEntity,
            };
            return Results.Problem(
                statusCode: statusCode,
                title: exception.Message,
                extensions: new Dictionary<string, object?> { ["code"] = exception.Code }
            );
        }
    }

    private static async Task<IResult> FindAsync(
        Guid id,
        ClaimsPrincipal user,
        FitActivityImportService service,
        CancellationToken cancellationToken
    )
    {
        var ownerSubject = user.FindFirstValue("sub");
        if (string.IsNullOrWhiteSpace(ownerSubject))
        {
            return Results.Forbid();
        }

        var activity = await service.FindAsync(id, ownerSubject, cancellationToken);
        return activity is null ? Results.NotFound() : Results.Ok(ToResponse(activity));
    }

    private static object ToResponse(Activity activity) =>
        new
        {
            activity.Id,
            type = activity.Type.Code,
            source = new
            {
                activity.Source.Kind,
                activity.Source.Provider,
                activity.Source.ExternalIdentifier,
                activity.Source.OriginalFileName,
                activity.Source.ContentHashSha256,
                activity.Source.ImportedAtUtc,
            },
            timeRange = new
            {
                start = ToTimestamp(activity.TimeRange.Start),
                end = ToTimestamp(activity.TimeRange.End),
            },
            summary = activity.Summary.Metrics.Select(ToMetric),
            samples = activity.Samples.Select(sample => new
            {
                sample.TimestampUtc,
                metrics = sample.Metrics.Select(ToMetric),
            }),
            segments = activity.Segments.Select(segment => new
            {
                segment.Index,
                type = segment.Type.Code,
                timeRange = new
                {
                    start = ToTimestamp(segment.TimeRange.Start),
                    end = ToTimestamp(segment.TimeRange.End),
                },
                summary = segment.Summary.Metrics.Select(ToMetric),
            }),
            quality = new
            {
                analysisStatus = activity.Quality.AnalysisStatus,
                activity.Quality.IsAnalysisRestricted,
                findings = activity.Quality.Findings.Select(finding => new
                {
                    finding.Code,
                    severity = finding.Severity.ToString().ToLowerInvariant(),
                    finding.Message,
                    finding.AffectedMetricCodes,
                }),
            },
        };

    private static object ToTimestamp(ActivityTimestamp timestamp) =>
        new
        {
            timestamp.InstantUtc,
            timestamp.TimeZoneId,
            observedUtcOffsetMinutes = timestamp.ObservedUtcOffset.TotalMinutes,
        };

    private static object ToMetric(ActivityMetric metric) =>
        new
        {
            metric.Code,
            metric.Value,
            unit = new { metric.Unit.Code, metric.Unit.Symbol },
            provenance = new
            {
                origin = metric.Provenance.Origin.ToString().ToLowerInvariant(),
                metric.Provenance.SourceField,
                metric.Provenance.SourceUnit,
                derivation = metric.Provenance.Derivation is null
                    ? null
                    : new
                    {
                        metric.Provenance.Derivation.Method,
                        metric.Provenance.Derivation.Version,
                        metric.Provenance.Derivation.InputMetricCodes,
                    },
            },
        };
}
