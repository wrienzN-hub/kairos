using Kairos.Domain;

namespace Kairos.Application;

public sealed class ApplicationAssemblyMarker
{
    public Type DomainMarkerType => typeof(DomainAssemblyMarker);
}
