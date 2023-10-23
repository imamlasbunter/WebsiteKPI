using Pertamina.Website_KPI.IntegrationTests.Application;
using Xunit;

namespace Pertamina.Website_KPI.IntegrationTests;
[CollectionDefinition(nameof(ApplicationFixture), DisableParallelization = true)]
public class LaunchSettingsCollection : ICollectionFixture<ApplicationFixture>
{
}
