namespace Kairos.Api.Authentication;

public sealed class AuthorityRewriteHandler(
    Uri publicAuthority,
    Uri backchannelAuthority) : HttpClientHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (request.RequestUri is not null
            && Uri.Compare(
                request.RequestUri,
                publicAuthority,
                UriComponents.SchemeAndServer,
                UriFormat.Unescaped,
                StringComparison.OrdinalIgnoreCase) == 0)
        {
            request.RequestUri = new Uri(
                backchannelAuthority,
                request.RequestUri.PathAndQuery);
            request.Headers.Host = null;
        }

        return base.SendAsync(request, cancellationToken);
    }
}
