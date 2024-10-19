using FirebirdSql.EntityFrameworkCore.Firebird.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Trace;
using System.Diagnostics.CodeAnalysis;

namespace Firebird.Aspire.EntityFrameworkCore.Client;

public static class FirebirdClientEFCoreExtensions
{
    private const string DefaultConfigSectionName = "Firebird:Aspire:EntityFrameworkCore:Client";
    private const DynamicallyAccessedMemberTypes RequiredByEF = DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties;

    /// <summary>
    /// Registers the given <see cref="DbContext" /> as a service in the services provided by the <paramref name="builder"/>.
    /// Enables db context pooling, retries, health check, logging and telemetry for the <see cref="DbContext" />.
    /// </summary>
    /// <typeparam name="TContext">The <see cref="DbContext" /> that needs to be registered.</typeparam>
    /// <param name="builder">The <see cref="IHostApplicationBuilder" /> to read config from and add services to.</param>
    /// <param name="connectionName">A name used to retrieve the connection string from the ConnectionStrings configuration section.</param>
    /// <param name="configureSettings">An optional delegate that can be used for customizing options. It's invoked after the settings are read from the configuration.</param>
    /// <param name="configureDbContextOptions">An optional delegate to configure the <see cref="DbContextOptions"/> for the context.</param>
    /// <remarks>Reads the configuration from "Firebird:Aspire:EntityFrameworkCore:Client:{typeof(TContext).Name}" config section, or "Firebird:Aspire:EntityFrameworkCore:Client" if former does not exist.</remarks>
    /// <exception cref="ArgumentNullException">Thrown if mandatory <paramref name="builder"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when mandatory <see cref="FirebirdClientEFCoreSettings.ConnectionString"/> is not provided.</exception>
    public static void AddFirebirdDbContext<[DynamicallyAccessedMembers(RequiredByEF)] TContext>(
        this IHostApplicationBuilder builder,
        string connectionName,
        Action<FirebirdClientEFCoreSettings>? configureSettings = null,
        Action<DbContextOptionsBuilder>? configureDbContextOptions = null) where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.EnsureDbContextNotRegistered<TContext>();

        var settings = builder.GetDbContextSettings<TContext, FirebirdClientEFCoreSettings>(
            DefaultConfigSectionName,
            (settings, section) => section.Bind(settings)
        );

        if (builder.Configuration.GetConnectionString(connectionName) is string connectionString)
        {
            settings.ConnectionString = connectionString;
        }

        configureSettings?.Invoke(settings);

        builder.Services.AddDbContextPool<TContext>(ConfigureDbContext);

        ConfigureInstrumentation<TContext>(builder, settings);

        void ConfigureDbContext(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            // We don't register logger factory, because there is no need to:
            // https://learn.microsoft.com/dotnet/api/microsoft.entityframeworkcore.dbcontextoptionsbuilder.useloggerfactory?view=efcore-7.0#remarks
            dbContextOptionsBuilder.UseFirebird(settings.ConnectionString, builder =>
            {
                //ConnectionStringValidation.ValidateConnectionString(settings.ConnectionString, connectionName, DefaultConfigSectionName, $"{DefaultConfigSectionName}:{typeof(TContext).Name}", isEfDesignTime: EF.IsDesignTime);

                // Resiliency:
                // Connection resiliency automatically retries failed database commands
                if (!settings.DisableRetry)
                {
                    builder.ExecutionStrategy(c => new FbRetryingExecutionStrategy(c));
                }

                // The time in seconds to wait for the command to execute.
                if (settings.CommandTimeout.HasValue)
                {
                    builder.CommandTimeout(settings.CommandTimeout);
                }
            });

            configureDbContextOptions?.Invoke(dbContextOptionsBuilder);
        }
    }

    /// <summary>
    /// Configures retries, health check, logging and telemetry for the <see cref="DbContext" />.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if mandatory <paramref name="builder"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when mandatory <see cref="DbContext"/> is not registered in DI.</exception>
    public static void EnrichFirebirdDbContext<[DynamicallyAccessedMembers(RequiredByEF)] TContext>(
            this IHostApplicationBuilder builder,
            Action<FirebirdClientEFCoreSettings>? configureSettings = null) where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(builder);

        var settings = builder.GetDbContextSettings<TContext, FirebirdClientEFCoreSettings>(
            DefaultConfigSectionName,
            (settings, section) => section.Bind(settings)
        );

        configureSettings?.Invoke(settings);

        ConfigureRetry();

        ConfigureInstrumentation<TContext>(builder, settings);

        void ConfigureRetry()
        {
#pragma warning disable EF1001 // Internal EF Core API usage.
            if (!settings.DisableRetry || settings.CommandTimeout.HasValue)
            {
                builder.PatchServiceDescriptor<TContext>(optionsBuilder => optionsBuilder.UseFirebird(settings.ConnectionString, options =>
                {
                    var extension = optionsBuilder.Options.FindExtension<FbOptionsExtension>();

                    if (!settings.DisableRetry)
                    {
                        var executionStrategy = extension?.ExecutionStrategyFactory?.Invoke(new ExecutionStrategyDependencies(null!, optionsBuilder.Options, null!));

                        if (executionStrategy != null)
                        {
                            if (executionStrategy is FbRetryingExecutionStrategy)
                            {
                                // Keep custom Retry strategy.
                                // Any sub-class of FbRetryingExecutionStrategy is a valid retry strategy
                                // which shouldn't be replaced even with DisableRetry == false
                            }
                            else if (executionStrategy.GetType() != typeof(FbRetryingExecutionStrategy))
                            {
                                // Check FbRetryingExecutionStrategy specifically (no 'is'), any sub-class is treated as a custom strategy.

                                throw new InvalidOperationException($"{nameof(FirebirdClientEFCoreSettings)}.{nameof(FirebirdClientEFCoreSettings.DisableRetry)} needs to be set when a custom Execution Strategy is configured.");
                            }
                            else
                            {
                                //options.EnableRetryOnFailure();
                                options.ExecutionStrategy(c => new FbRetryingExecutionStrategy(c));
                            }
                        }
                        else
                        {
                            //options.EnableRetryOnFailure();
                            options.ExecutionStrategy(c => new FbRetryingExecutionStrategy(c));
                        }
                    }

                    if (settings.CommandTimeout.HasValue)
                    {
                        if (extension != null &&
                            extension.CommandTimeout.HasValue &&
                            extension.CommandTimeout != settings.CommandTimeout)
                        {
                            throw new InvalidOperationException($"Conflicting values for 'CommandTimeout' were found in {nameof(FirebirdClientEFCoreSettings)} and set in DbContextOptions<{typeof(TContext).Name}>.");
                        }

                        options.CommandTimeout(settings.CommandTimeout);
                    }
                }));
            }
#pragma warning restore EF1001 // Internal EF Core API usage.
        }
    }

    private static void ConfigureInstrumentation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties)] TContext>(
        IHostApplicationBuilder builder,
        FirebirdClientEFCoreSettings settings) where TContext : DbContext
    {
        if (!settings.DisableTracing)
        {
            builder.Services.AddOpenTelemetry()
                .WithTracing(builder => builder
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddConsoleExporter());
        }

        if (!settings.DisableHealthChecks)
        {
            builder.TryAddHealthCheck(
                name: typeof(TContext).Name,
                static hcBuilder => hcBuilder.AddDbContextCheck<TContext>());
        }
    }
}
