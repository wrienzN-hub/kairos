using Kairos.Application;

namespace Kairos.UnitTests;

public sealed class ModuleBoundaryTests
{
    [Fact]
    public void Application_references_domain_without_referencing_infrastructure()
    {
        var references = typeof(ApplicationAssemblyMarker)
            .Assembly
            .GetReferencedAssemblies()
            .Select(reference => reference.Name)
            .ToArray();

        Assert.Contains("Kairos.Domain", references);
        Assert.DoesNotContain("Kairos.Infrastructure", references);
    }
}
