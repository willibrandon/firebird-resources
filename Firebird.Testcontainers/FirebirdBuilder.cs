namespace Firebird.Testcontainers;

/// <inheritdoc cref="ContainerBuilder{TBuilderEntity, TContainerEntity, TConfigurationEntity}" />
[PublicAPI]
public sealed class FirebirdBuilder : ContainerBuilder<FirebirdBuilder, FirebirdContainer, FirebirdConfiguration>
{
    /// <summary>firebird</summary>
    public const string DefaultDatabase = "firebird";

    /// <summary>/var/lib/firebird/data/</summary>
    public const string DefaultDatabaseLocation = "/var/lib/firebird/data/";

    /// <summary>masterkey</summary>
    public const string DefaultPassword = "masterkey";

    /// <summary>sysdba</summary>
    public const string DefaultUsername = "sysdba";

    /// <summary>fdcastel/firebird</summary>
    public const string Image = "fdcastel/firebird";

    /// <summary>3050</summary>
    public const ushort Port = 3050;

    /// <summary>docker.io</summary>
    public const string Registry = "ghcr.io";

    /// <summary>latest</summary>
    public const string Tag = "latest";

    /// <inheritdoc />
    protected override FirebirdConfiguration DockerResourceConfiguration { get; }

    /// <summary>
    ///  Initializes a new instance of the <see cref="FirebirdBuilder" /> class.
    /// </summary>
    public FirebirdBuilder() : this(new FirebirdConfiguration())
        => DockerResourceConfiguration = Init().DockerResourceConfiguration;

    /// <summary>
    ///  Initializes a new instance of the <see cref="FirebirdBuilder" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    private FirebirdBuilder(FirebirdConfiguration resourceConfiguration) : base(resourceConfiguration)
        => DockerResourceConfiguration = resourceConfiguration;

    /// <inheritdoc />
    protected override FirebirdBuilder Clone(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
        => Merge(DockerResourceConfiguration, new FirebirdConfiguration(resourceConfiguration));

    /// <inheritdoc />
    protected override FirebirdBuilder Clone(IContainerConfiguration resourceConfiguration)
        => Merge(DockerResourceConfiguration, new FirebirdConfiguration(resourceConfiguration));

    /// <inheritdoc />
    protected override FirebirdBuilder Init()
        => base.Init()
            .WithImage(Registry + "/" + Image + ":" + Tag)
            .WithPortBinding(Port, true)
            .WithDatabase(DefaultDatabase)
            .WithPassword(DefaultPassword)
            .WithRootPassword(DefaultPassword)
            .WithUsername(DefaultUsername)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(Port));

    /// <inheritdoc />
    protected override void Validate()
    {
        base.Validate();

        // If a Firebird username is set, you must set a password. Otherwise, container initialization will fail.
        if (DockerResourceConfiguration.Username != null)
        {
            _ = Guard.Argument(DockerResourceConfiguration.Password, nameof(DockerResourceConfiguration.Password))
                .NotNull()
                .NotEmpty();
        }
    }

    /// <inheritdoc />
    protected override FirebirdBuilder Merge(FirebirdConfiguration oldValue, FirebirdConfiguration newValue)
        => new(new FirebirdConfiguration(oldValue, newValue));

    /// <inheritdoc />
    public override FirebirdContainer Build()
    {
        Validate();
        return new FirebirdContainer(DockerResourceConfiguration);
    }

    /// <summary>
    ///  Sets values in the Firebird configuration file (firebird.conf).
    /// </summary>
    /// <param name="entry">The name of the configuration entry.</param>
    /// <param name="value">The value of configuration entry.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithConfiguration(string entry, string value)
        => Merge(DockerResourceConfiguration, new FirebirdConfiguration())
            .WithEnvironment("FIREBIRD_CONF_" + entry, value);

    /// <summary>
    ///  Sets the Firebird database name.
    /// </summary>
    /// <param name="database">The Firebird database.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithDatabase(string database)
        => Merge(DockerResourceConfiguration, new FirebirdConfiguration(database: database))
            .WithEnvironment("FIREBIRD_DATABASE", database);

    /// <summary>
    ///  Sets the Firebird user password.
    /// </summary>
    /// <param name="password">The Firebird password.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithPassword(string password)
        => Merge(DockerResourceConfiguration, new FirebirdConfiguration(password: password))
            .WithEnvironment("FIREBIRD_PASSWORD", password);

    /// <summary>
    ///  Sets the Firebird SYSDBA password.
    /// </summary>
    /// <param name="rootPassword">The Firebird password.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithRootPassword(string rootPassword)
        => Merge(DockerResourceConfiguration, new FirebirdConfiguration())
            .WithEnvironment("FIREBIRD_ROOT_PASSWORD", rootPassword);

    /// <summary>
    ///  Sets the Firebird container time zone. e.g. "America/Los_Angeles".
    /// </summary>
    /// <remarks>
    /// <para>
    ///  The container runs in UTC time zone by default.
    /// </para>
    /// </remarks>
    /// <param name="value">The Firebird password.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithTimeZone(string value)
        => Merge(DockerResourceConfiguration, new FirebirdConfiguration())
            .WithEnvironment("TZ", value);

    /// <summary>
    ///  Enables legacy Firebird authentication (not recommended).
    /// </summary>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithUseLegacyAuth()
        => WithConfiguration("FIREBIRD_USE_LEGACY_AUTH", true.ToString());

    /// <summary>
    ///  Sets the Firebird user name.
    /// </summary>
    /// <param name="username">The Firebird username.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithUsername(string username)
        => Merge(DockerResourceConfiguration, new FirebirdConfiguration(username: username))
            .WithEnvironment("FIREBIRD_USER", username);
}