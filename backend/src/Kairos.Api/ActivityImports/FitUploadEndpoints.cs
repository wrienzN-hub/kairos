using System.Security.Claims;
using Kairos.Application.ActivityImports;

namespace Kairos.Api.ActivityImports;

public static class FitUploadEndpoints
{
    public static IEndpointRouteBuilder MapFitUploadEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/activity-imports/fit").RequireAuthorization();

        group.MapPost("/", UploadAsync).DisableAntiforgery();
        group.MapGet("/{id:guid}", FindAsync);

        return endpoints;
    }

    private static async Task<IResult> UploadAsync(
        HttpRequest request,
        ClaimsPrincipal user,
        FitUploadService service,
        CancellationToken cancellationToken
    )
    {
        var ownerSubject = user.FindFirstValue("sub");
        if (string.IsNullOrWhiteSpace(ownerSubject))
        {
            return Results.Problem(
                statusCode: StatusCodes.Status403Forbidden,
                title: "Der angemeldete Benutzer besitzt keine gültige Kennung.",
                extensions: new Dictionary<string, object?> { ["code"] = "missing_subject" }
            );
        }

        if (!request.HasFormContentType)
        {
            return UploadProblem(
                new FitUploadException(
                    "multipart_required",
                    "Der Upload muss als multipart/form-data gesendet werden.",
                    StatusCodes.Status415UnsupportedMediaType
                )
            );
        }

        try
        {
            var form = await request.ReadFormAsync(cancellationToken);
            if (form.Files.Count != 1 || form.Files.GetFile("file") is not { } file)
            {
                throw new FitUploadException(
                    "single_file_required",
                    "Bitte genau eine FIT-Datei im Formularfeld 'file' hochladen.",
                    StatusCodes.Status400BadRequest
                );
            }

            await using var content = file.OpenReadStream();
            var receipt = await service.UploadAsync(
                ownerSubject,
                file.FileName,
                file.ContentType,
                file.Length,
                content,
                cancellationToken
            );

            return Results.Created($"/api/activity-imports/fit/{receipt.Id}", receipt);
        }
        catch (FitUploadException exception)
        {
            return UploadProblem(exception);
        }
        catch (InvalidDataException)
        {
            return UploadProblem(
                new FitUploadException(
                    "invalid_multipart_upload",
                    "Der Upload ist zu groß oder enthält ungültige Formulardaten.",
                    StatusCodes.Status413PayloadTooLarge
                )
            );
        }
    }

    private static async Task<IResult> FindAsync(
        Guid id,
        ClaimsPrincipal user,
        FitUploadService service,
        CancellationToken cancellationToken
    )
    {
        var ownerSubject = user.FindFirstValue("sub");
        if (string.IsNullOrWhiteSpace(ownerSubject))
        {
            return Results.Forbid();
        }

        var receipt = await service.FindAsync(id, ownerSubject, cancellationToken);
        return receipt is null ? Results.NotFound() : Results.Ok(receipt);
    }

    private static IResult UploadProblem(FitUploadException exception)
    {
        return Results.Problem(
            statusCode: exception.StatusCode,
            title: exception.Message,
            extensions: new Dictionary<string, object?> { ["code"] = exception.Code }
        );
    }
}
