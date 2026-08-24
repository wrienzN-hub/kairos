using Kairos.Domain.Activities;

namespace Kairos.Application.ActivityImports;

public sealed record FitActivityFile(
    Guid UploadId,
    string OriginalFileName,
    string Sha256,
    DateTimeOffset UploadedAtUtc,
    byte[] Content
);

public interface IFitActivityParser
{
    Activity Parse(FitActivityFile file);
}

public sealed class FitParseException : Exception
{
    public string Code { get; }

    public FitParseException(string code, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        Code = code;
    }
}
