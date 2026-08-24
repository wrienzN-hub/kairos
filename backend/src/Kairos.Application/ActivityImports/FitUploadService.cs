using System.Security.Cryptography;

namespace Kairos.Application.ActivityImports;

public sealed class FitUploadService(IFitUploadStore store, FitUploadPolicy policy)
{
    private static readonly HashSet<string> SupportedContentTypes =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "application/octet-stream",
            "application/fit",
            "application/vnd.ant.fit",
        };

    public async Task<FitUploadReceipt> UploadAsync(
        string ownerSubject,
        string suppliedFileName,
        string? contentType,
        long reportedLength,
        Stream content,
        CancellationToken cancellationToken
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ownerSubject);
        ArgumentNullException.ThrowIfNull(content);

        var fileName = NormalizeFileName(suppliedFileName);
        ValidateMetadata(fileName, contentType, reportedLength);

        var bytes = await ReadBoundedAsync(content, cancellationToken);
        if (bytes.LongLength != reportedLength)
        {
            throw new FitUploadException(
                "upload_length_mismatch",
                "Die übertragene Dateigröße stimmt nicht mit den Upload-Angaben überein.",
                400
            );
        }

        FitFileValidator.Validate(bytes);

        var submission = new FitUploadSubmission(
            Guid.NewGuid(),
            ownerSubject,
            fileName,
            NormalizeContentType(contentType),
            bytes.LongLength,
            Convert.ToHexStringLower(SHA256.HashData(bytes)),
            DateTimeOffset.UtcNow,
            bytes
        );

        await store.AddAsync(submission, cancellationToken);
        return ToReceipt(submission);
    }

    public Task<FitUploadReceipt?> FindAsync(
        Guid id,
        string ownerSubject,
        CancellationToken cancellationToken
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ownerSubject);
        return store.FindAsync(id, ownerSubject, cancellationToken);
    }

    private void ValidateMetadata(string fileName, string? contentType, long reportedLength)
    {
        if (!string.Equals(Path.GetExtension(fileName), ".fit", StringComparison.OrdinalIgnoreCase))
        {
            throw new FitUploadException(
                "unsupported_file_type",
                "Bitte eine Datei mit der Endung .fit auswählen.",
                415
            );
        }

        if (!string.IsNullOrWhiteSpace(contentType) && !SupportedContentTypes.Contains(contentType))
        {
            throw new FitUploadException(
                "unsupported_media_type",
                "Der Medientyp der Datei wird nicht als FIT-Datei unterstützt.",
                415
            );
        }

        if (reportedLength <= 0)
        {
            throw new FitUploadException("empty_file", "Die ausgewählte Datei ist leer.", 400);
        }

        if (reportedLength > policy.MaximumFileSizeBytes)
        {
            throw TooLarge();
        }
    }

    private async Task<byte[]> ReadBoundedAsync(Stream source, CancellationToken cancellationToken)
    {
        using var destination = new MemoryStream();
        var buffer = new byte[64 * 1024];

        while (true)
        {
            var read = await source.ReadAsync(buffer, cancellationToken);
            if (read == 0)
            {
                break;
            }

            if (destination.Length + read > policy.MaximumFileSizeBytes)
            {
                throw TooLarge();
            }

            await destination.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
        }

        return destination.ToArray();
    }

    private static string NormalizeFileName(string suppliedFileName)
    {
        if (string.IsNullOrWhiteSpace(suppliedFileName))
        {
            throw new FitUploadException("missing_file_name", "Der Dateiname fehlt.", 400);
        }

        var normalizedSeparators = suppliedFileName.Replace('\\', '/');
        var fileName = normalizedSeparators[(normalizedSeparators.LastIndexOf('/') + 1)..].Trim();
        if (
            fileName.Length is 0 or > 255
            || fileName.Any(character => char.IsControl(character) || character == '\0')
        )
        {
            throw new FitUploadException("invalid_file_name", "Der Dateiname ist ungültig.", 400);
        }

        return fileName;
    }

    private static string NormalizeContentType(string? contentType)
    {
        return string.IsNullOrWhiteSpace(contentType) ? "application/octet-stream" : contentType;
    }

    private FitUploadException TooLarge()
    {
        return new FitUploadException(
            "file_too_large",
            $"Die FIT-Datei darf höchstens {policy.MaximumFileSizeBytes} Bytes groß sein.",
            413
        );
    }

    private static FitUploadReceipt ToReceipt(FitUploadSubmission upload)
    {
        return new FitUploadReceipt(
            upload.Id,
            upload.OriginalFileName,
            upload.SizeBytes,
            upload.Sha256,
            upload.UploadedAtUtc,
            "pending"
        );
    }
}
