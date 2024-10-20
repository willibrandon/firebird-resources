using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace FirebirdResources.Aspire.Client;

/// <summary>
/// Extension methods for connecting Firebird database with FirebirdSql.Data.FirebirdClient.
/// </summary>
public static class FirebirdClientExtensions
{
    /// <summary>
    /// Registers 'Scoped' <see cref="FirebirdConnectionFactory" /> for connecting Firebird database using FirebirdSql.Data.FirebirdClient.
    /// Configures health check, logging and telemetry for the FirebirdClient.
    /// </summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder" /> to read config from and add services to.</param>
    /// <param name="connectionName">A name used to retrieve the connection string from the ConnectionStrings configuration section.</param>
    /// <param name="configureSettings">An optional delegate that can be used for customizing options. It's invoked after the settings are read from the configuration.</param>
    /// <remarks>Reads the configuration from "Firebird:Aspire:Client" section.</remarks>
    /// <exception cref="InvalidOperationException">If required <see cref="FirebirdClientSettings.ConnectionString"/>  is not provided in configuration section.</exception>
    public static void AddFirebirdClient(
        this IHostApplicationBuilder builder,
        string connectionName,
        Action<FirebirdClientSettings>? configureSettings = null) =>
        AddFirebirdClient(
            builder,
            FirebirdClientSettings.DefaultConfigSectionName,
            configureSettings,
            connectionName,
            serviceKey: null);

    /// <summary>
    /// Registers 'Scoped' <see cref="FirebirdConnectionFactory" /> for given <paramref name="name"/> for connecting Firebird database using FirebirdSql.Data.FirebirdClient.
    /// Configures health check, logging and telemetry for the FirebirdClient.
    /// </summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder" /> to read config from and add services to.</param>
    /// <param name="name">The name of the component, which is used as the <see cref="ServiceDescriptor.ServiceKey"/> of the service and also to retrieve the connection string from the ConnectionStrings configuration section.</param>
    /// <param name="configureSettings">An optional method that can be used for customizing options. It's invoked after the settings are read from the configuration.</param>
    /// <remarks>Reads the configuration from "Firebird:Aspire:Client:{name}" section.</remarks>
    /// <exception cref="InvalidOperationException">If required <see cref="FirebirdClientSettings.ConnectionString"/> is not provided in configuration section.</exception>
    public static void AddKeyedFirebirdClient(
        this IHostApplicationBuilder builder,
        string name,
        Action<FirebirdClientSettings>? configureSettings = null)
    {
        ArgumentNullException.ThrowIfNull(name);

        AddFirebirdClient(
            builder,
            $"{FirebirdClientSettings.DefaultConfigSectionName}:{name}",
            configureSettings,
            connectionName: name,
            serviceKey: name);
    }

    private static void AddFirebirdClient(
        this IHostApplicationBuilder builder,
        string configurationSectionName,
        Action<FirebirdClientSettings>? configureSettings,
        string connectionName,
        object? serviceKey)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var settings = new FirebirdClientSettings();

        builder.Configuration
               .GetSection(configurationSectionName)
               .Bind(settings);

        if (builder.Configuration.GetConnectionString(connectionName) is string connectionString)
        {
            settings.ConnectionString = connectionString;
        }

        configureSettings?.Invoke(settings);

        if (serviceKey is null)
        {
            builder.Services.AddScoped(CreateFbConnectionFactory);
        }
        else
        {
            builder.Services.AddKeyedScoped(serviceKey, (sp, key) => CreateFbConnectionFactory(sp));
        }

        FirebirdConnectionFactory CreateFbConnectionFactory(IServiceProvider _)
        {
            return new FirebirdConnectionFactory(settings);
        }

        if (settings.DisableHealthChecks is false)
        {
            builder.TryAddHealthCheck(new HealthCheckRegistration(
                serviceKey is null ? "Firebird" : $"Firebird_{connectionName}",
                sp => new FirebirdHealthCheck(
                    serviceKey is null
                        ? sp.GetRequiredService<FirebirdConnectionFactory>()
                        : sp.GetRequiredKeyedService<FirebirdConnectionFactory>(serviceKey)),
                failureStatus: default,
                tags: default,
                timeout: default));
        }

        // TODO
        if (settings.DisableTracing is false)
        {
            //builder.Services.AddOpenTelemetry()
            //    .WithTracing(tracerProviderBuilder =>
            //    {
            //        tracerProviderBuilder.AddNpgsql();
            //    });
        }

        // TODO
        if (settings.DisableMetrics is false)
        {
            //builder.Services.AddOpenTelemetry()
            //    .WithMetrics(NpgsqlCommon.AddNpgsqlMetrics);
        }
    }
}
