namespace Firebird.Aspire.Client.Tests;

public class ConfigurationTests
{
    [Fact]
    public void ConnectionStringIsNullByDefault()
    => Assert.Null(new FirebirdSettings().ConnectionString);

    [Fact]
    public void HealthCheckIsEnabledByDefault()
        => Assert.False(new FirebirdSettings().DisableHealthChecks);

    [Fact]
    public void TracingIsEnabledByDefault()
        => Assert.False(new FirebirdSettings().DisableTracing);

    [Fact]
    public void MetricsAreEnabledByDefault()
        => Assert.False(new FirebirdSettings().DisableMetrics);
}
