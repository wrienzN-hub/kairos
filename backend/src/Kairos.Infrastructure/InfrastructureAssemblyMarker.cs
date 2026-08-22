using Kairos.Application;

namespace Kairos.Infrastructure;

public sealed class InfrastructureAssemblyMarker
{
    public Type ApplicationMarkerType => typeof(ApplicationAssemblyMarker);
}
