
namespace FirebirdResource.Tests;

public class DistributedApplicationFixture : IAsyncLifetime
{
    private DistributedApplication? _app;

    public DistributedApplication App => _app ?? throw new InvalidOperationException("The application has not been initialized.");

    public async Task InitializeAsync()
    {
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.FirebirdResource_AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        _app = await appHost.BuildAsync();
        var resourceNotificationService = _app.Services.GetRequiredService<ResourceNotificationService>();
        await _app.StartAsync();

        await resourceNotificationService.WaitForResourceAsync("apiService", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));
    }

    public Task DisposeAsync()
    {
        _app?.Dispose();
        return Task.CompletedTask;
    }
}
