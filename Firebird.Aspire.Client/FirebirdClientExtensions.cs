using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Firebird.Aspire.Client;

/// <summary>
/// Extension methods for connecting Firebird database with FirebirdSql.Data.FirebirdClient.
/// </summary>
public static class FirebirdClientExtensions
{
    /// <summary>
    /// Registers 'Scoped' <see cref="FbConnection" /> factory for connecting Firebird database using FirebirdSql.Data.FirebirdClient.
    /// Configures health check, logging and telemetry for the FirebirdClient.
    /// </summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder" /> to read config from and add services to.</param>
    /// <param name="connectionName">A name used to retrieve the connection string from the ConnectionStrings configuration section.</param>
    /// <param name="configureSettings">An optional delegate that can be used for customizing options. It's invoked after the settings are read from the configuration.</param>
    /// <remarks>Reads the configuration from "Aspire:Microsoft:Data:SqlClient" section.</remarks>
    /// <exception cref="InvalidOperationException">If required <see cref="FirebirdSettings.ConnectionString"/>  is not provided in configuration section.</exception>
    public static void AddFirebirdClient(
        this IHostApplicationBuilder builder,
        string connectionName,
        Action<FirebirdSettings>? configureSettings = null) =>
        AddFirebirdClient(
            builder,
            FirebirdSettings.DefaultConfigSectionName,
            configureSettings,
            connectionName,
            serviceKey: null);

    /// <summary>
    /// Registers 'Scoped' <see cref="SqlConnection" /> factory for given <paramref name="name"/> for connecting Azure SQL, MsSQL database using Microsoft.Data.SqlClient.
    /// Configures health check, logging and telemetry for the SqlClient.
    /// </summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder" /> to read config from and add services to.</param>
    /// <param name="name">The name of the component, which is used as the <see cref="ServiceDescriptor.ServiceKey"/> of the service and also to retrieve the connection string from the ConnectionStrings configuration section.</param>
    /// <param name="configureSettings">An optional method that can be used for customizing options. It's invoked after the settings are read from the configuration.</param>
    /// <remarks>Reads the configuration from "Aspire:Microsoft:Data:SqlClient:{name}" section.</remarks>
    /// <exception cref="InvalidOperationException">If required <see cref="FirebirdSettings.ConnectionString"/> is not provided in configuration section.</exception>
    public static void AddKeyedFirebirdClient(
        this IHostApplicationBuilder builder,
        string name,
        Action<FirebirdSettings>? configureSettings = null)
    {
        ArgumentNullException.ThrowIfNull(name);

        AddFirebirdClient(
            builder,
            $"{FirebirdSettings.DefaultConfigSectionName}:{name}",
            configureSettings,
            connectionName: name,
            serviceKey: name);
    }

    private static void AddFirebirdClient(
        this IHostApplicationBuilder builder,
        string configurationSectionName,
        Action<FirebirdSettings>? configureSettings,
        string connectionName,
        object? serviceKey)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var settings = new FirebirdSettings();

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

        FbConnectionFactory CreateFbConnectionFactory(IServiceProvider _)
        {
            return new FbConnectionFactory(settings);
        }

        if (settings.DisableHealthChecks is false)
        {
            builder.Services.AddHealthChecks()
                .AddCheck<FirebirdHealthCheck>(
                    name: serviceKey is null ? "Firebird" : $"Firebird_{connectionName}",
                    failureStatus: default,
                    tags: []);
        }

        // TODO
        //if (settings.DisableTracing is false)
        //{
        //    builder.Services.AddOpenTelemetry()
        //        .WithTracing(
        //            traceBuilder => traceBuilder.AddSource(
        //                Telemetry.SmtpClient.ActivitySourceName));
        //}

        //if (settings.DisableMetrics is false)
        //{
        //    // Required by MailKit to enable metrics
        //    Telemetry.SmtpClient.Configure();

        //    builder.Services.AddOpenTelemetry()
        //        .WithMetrics(
        //            metricsBuilder => metricsBuilder.AddMeter(
        //                Telemetry.SmtpClient.MeterName));
        //}
    }
}
