namespace Kairos.Application.ActivityImports;

public sealed class FitUploadException : Exception
{
    public FitUploadException(string code, string message, int statusCode)
        : base(message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        Code = code;
        StatusCode = statusCode;
    }

    public string Code { get; }

    public int StatusCode { get; }
}
