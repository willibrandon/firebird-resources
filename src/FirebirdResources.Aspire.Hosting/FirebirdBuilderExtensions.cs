﻿using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using FirebirdResources.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace FirebirdResources.Aspire.Hosting;

/// <summary>
/// Provides extension methods for adding Firebird resources to an <see cref="IDistributedApplicationBuilder"/>.
/// </summary>
public static class FirebirdBuilderExtensions
{
    private const string DefaultDatabaseName = "firebird";
    private const string DefaultSysDbaPassword = "masterkey";

    /// <summary>
    ///  Adds a Firebird resource to the application model. A container is used for local development. This version of the package defaults to the
    /// <inheritdoc cref="FirebirdContainerImageTags.Tag"/> tag of the <inheritdoc cref="FirebirdContainerImageTags.Image"/> container image.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/>.</param>
    /// <param name="name">The name of the resource. This name will be used as the connection string name when referenced in a dependency.</param>
    /// <param name="userName">The parameter used to provide the user name for the Firebird resource. If <see langword="null"/> a default value will be used.</param>
    /// <param name="password">The parameter used to provide the administrator password for the Firebird resource. If <see langword="null"/> a random password will be generated.</param>
    /// <param name="port">The host port used when launching the container. If null a random port will be assigned.</param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    /// <remarks>
    /// <para>
    /// This resource includes built-in health checks. When this resource is referenced as a dependency
    /// using the <see cref="ResourceBuilderExtensions.WaitFor{T}(IResourceBuilder{T}, IResourceBuilder{IResource})"/>
    /// extension method then the dependent resource will wait until the Firebird resource is able to service
    /// requests.
    /// </para>
    /// </remarks>
    public static IResourceBuilder<FirebirdServerResource> AddFirebird(this IDistributedApplicationBuilder builder,
        string name,
        IResourceBuilder<ParameterResource>? userName = null,
        IResourceBuilder<ParameterResource>? password = null,
        int? port = null)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(name);

        var passwordParameter = password?.Resource ?? ParameterResourceBuilderExtensions.CreateDefaultPasswordParameter(builder, DefaultSysDbaPassword);

        var firebirdServer = new FirebirdServerResource(name, userName?.Resource, passwordParameter);

        return builder.AddResource(firebirdServer)
                      .WithEndpoint(port: port, targetPort: 3050, name: FirebirdServerResource.PrimaryEndpointName) // Internal port is always 3050.
                      .WithImage(FirebirdContainerImageTags.Image, FirebirdContainerImageTags.Tag)
                      .WithImageRegistry(FirebirdContainerImageTags.Registry)
                      .WithEnvironment(context =>
                      {
                          context.EnvironmentVariables["FIREBIRD_DATABASE"] = DefaultDatabaseName;
                          context.EnvironmentVariables["FIREBIRD_ROOT_PASSWORD"] = DefaultSysDbaPassword;
                          context.EnvironmentVariables["FIREBIRD_USER"] = firebirdServer.UserNameReference;
                          context.EnvironmentVariables["FIREBIRD_PASSWORD"] = firebirdServer.PasswordParameter;
                      });
    }

    /// <summary>
    ///  Adds a Firebird database to the application model.
    /// </summary>
    /// <param name="builder">The Firebird server resource builder.</param>
    /// <param name="name">The name of the resource. This name will be used as the connection string name when referenced in a dependency.</param>
    /// <param name="databaseName">The name of the database. If not provided, this defaults to the same value as <paramref name="name"/>.</param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    /// <remarks>
    /// <para>
    /// This resource includes built-in health checks. When this resource is referenced as a dependency
    /// using the <see cref="ResourceBuilderExtensions.WaitFor{T}(IResourceBuilder{T}, IResourceBuilder{IResource})"/>
    /// extension method then the dependent resource will wait until the Firebird database is available.
    /// </para>
    /// <para>
    /// Note that by default calling <see cref="AddDatabase(IResourceBuilder{FirebirdServerResource}, string, string?)"/>
    /// does not result in the database being created on the Firebird server. It is expected that code within your solution
    /// will create the database. As a result if <see cref="ResourceBuilderExtensions.WaitFor{T}(IResourceBuilder{T}, IResourceBuilder{IResource})"/>
    /// is used with this resource it will wait indefinitely until the database exists.
    /// </para>
    /// </remarks>
    public static IResourceBuilder<FirebirdDatabaseResource> AddDatabase(
        this IResourceBuilder<FirebirdServerResource> builder,
        string name,
        string? databaseName = null)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(name);

        // Use the resource name as the database name if it's not provided
        databaseName ??= name;

        builder.Resource.AddDatabase(name, databaseName);
        builder.WithEnvironment("FIREBIRD_DATABASE", databaseName);
        var firebirdDatabase = new FirebirdDatabaseResource(name, databaseName, builder.Resource);

        string? connectionString = null;
        builder.ApplicationBuilder.Eventing.Subscribe<ConnectionStringAvailableEvent>(firebirdDatabase, async (@event, ct) =>
        {
            connectionString = await firebirdDatabase.GetConnectionStringAsync(ct).ConfigureAwait(false);

            if (connectionString == null)
            {
                throw new DistributedApplicationException($"ConnectionStringAvailableEvent was published for the '{firebirdDatabase.Name}' resource but the connection string was null.");
            }
        });

        var healthCheckKey = $"{name}_check";
        builder.ApplicationBuilder.Services.AddHealthChecks().AddFirebird(sp => connectionString ?? throw new InvalidOperationException("Connection string is unavailable"), name: healthCheckKey);

        return builder.ApplicationBuilder.AddResource(firebirdDatabase)
            .WithHealthCheck(healthCheckKey);
    }

    /// <summary>
    ///  Sets values in the Firebird configuration file (firebird.conf).
    /// </summary>
    /// <param name="builder">The resource builder.</param>
    /// <param name="entry">The name of the configuration entry.</param>
    /// <param name="value">The value of configuration entry.</param>
    /// <returns>The <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<FirebirdServerResource> WithConfig(
        this IResourceBuilder<FirebirdServerResource> builder,
        string entry,
        string value)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(entry);
        ArgumentNullException.ThrowIfNull(value);

        return builder.WithEnvironment("FIREBIRD_CONF" + entry, value);
    }

    /// <summary>
    ///  Adds a named volume for the data folder to a Firebird container resource.
    /// </summary>
    /// <param name="builder">The resource builder.</param>
    /// <param name="name">The name of the volume. Defaults to an auto-generated name based on the application and resource names.</param>
    /// <param name="isReadOnly">A flag that indicates if this is a read-only volume.</param>
    /// <returns>The <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<FirebirdServerResource> WithDataVolume(
        this IResourceBuilder<FirebirdServerResource> builder,
        string name,
        bool isReadOnly = false)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(name);

        return builder.WithVolume(name, "/var/lib/firebird/data", isReadOnly);
    }

    /// <summary>
    /// Adds a bind mount for the data folder to a Firebird container resource.
    /// </summary>
    /// <param name="builder">The resource builder.</param>
    /// <param name="source">The source directory on the host to mount into the container.</param>
    /// <param name="isReadOnly">A flag that indicates if this is a read-only mount.</param>
    /// <returns>The <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<FirebirdServerResource> WithDataBindMount(
        this IResourceBuilder<FirebirdServerResource> builder,
        string source,
        bool isReadOnly = false)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(source);

        return builder.WithBindMount(source, "/var/lib/firebird/data", isReadOnly);
    }

    /// <summary>
    ///  Adds a bind mount for the init folder to a Firebird container resource.
    /// </summary>
    /// <remarks>
    ///  When creating a new database with the <see cref="AddDatabase(IResourceBuilder{FirebirdServerResource}, string, string?)"/> method, you
    ///  can initialize it running one or more shell or SQL scripts. Any file with extensions .sh, .sql, .sql.gz, .sql.xz, and .sql.zst found
    ///  in <paramref name="source"/> will be executed in alphabetical order. .sh files without file execute permission (+x) will be sourced
    ///  rather than executed.
    /// </remarks>
    /// <param name="builder">The resource builder.</param>
    /// <param name="source">The source directory on the host to mount into the container.</param>
    /// <param name="isReadOnly">A flag that indicates if this is a read-only mount.</param>
    /// <returns>The <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<FirebirdServerResource> WithInitBindMount(
        this IResourceBuilder<FirebirdServerResource> builder,
        string source,
        bool isReadOnly = true)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(source);

        return builder.WithBindMount(source, "/docker-entrypoint-initdb.d", isReadOnly);
    }

    /// <summary>
    ///  Sets the Firebird user password.
    /// </summary>
    /// <param name="builder">The resource builder.</param>
    /// <param name="password">The Firebird user password.</param>
    /// <returns>The <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<FirebirdServerResource> WithPassword(this IResourceBuilder<FirebirdServerResource> builder, string password)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(password);

        return builder.WithEnvironment("FIREBIRD_PASSWORD", password);
    }

    /// <summary>
    ///  Sets the Firebird SYSDBA password.
    /// </summary>
    /// <param name="builder">The resource builder.</param>
    /// <param name="password">The Firebird SYSDBA password.</param>
    /// <returns>The <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<FirebirdServerResource> WithRootPassword(this IResourceBuilder<FirebirdServerResource> builder, string password)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(password);

        return builder.WithEnvironment("FIREBIRD_ROOT_PASSWORD", password);
    }

    /// <summary>
    ///  Sets the Firebird container time zone. e.g. "America/Los_Angeles".
    /// </summary>
    /// <param name="builder">The resource builder.</param>
    /// <param name="timezone">The Firebird container time zone.</param>
    /// <returns>The <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<FirebirdServerResource> WithTimeZone(this IResourceBuilder<FirebirdServerResource> builder, string timezone)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(timezone);

        return builder.WithEnvironment("TZ", timezone);
    }

    /// <summary>
    ///  Enables legacy Firebird authentication (not recommended).
    /// </summary>
    /// <param name="builder">The resource builder.</param>
    /// <returns>The <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<FirebirdServerResource> WithUseLegacyAuth(this IResourceBuilder<FirebirdServerResource> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.WithEnvironment("FIREBIRD_USE_LEGACY_AUTH", true.ToString());
    }

    /// <summary>
    ///  Creates a user in the Firebird security database.
    /// </summary>
    /// <param name="builder">The resource builder.</param>
    /// <param name="username">The Firebird username.</param>
    /// <returns>The <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<FirebirdServerResource> WithUsername(this IResourceBuilder<FirebirdServerResource> builder, string username)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(username);

        return builder.WithEnvironment("FIREBIRD_USER", username);
    }
}
