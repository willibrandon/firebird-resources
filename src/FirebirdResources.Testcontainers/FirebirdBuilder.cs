namespace FirebirdResources.Testcontainers;

/// <inheritdoc cref="ContainerBuilder{TBuilderEntity, TContainerEntity, TConfigurationEntity}" />
[PublicAPI]
public sealed class FirebirdBuilder : ContainerBuilder<FirebirdBuilder, FirebirdContainer, FirebirdConfiguration>
{
    /// <summary>firebird</summary>
    public const string DefaultDatabase = "firebird";

    /// <summary>/var/lib/firebird/data/</summary>
    public const string DefaultDatabaseLocation = "/var/lib/firebird/data/";

    /// <summary>firebird</summary>
    public const string DefaultPassword = "firebird";

    /// <summary>firebird</summary>
    public const string DefaultUsername = "firebird";

    /// <summary>fdcastel/firebird</summary>
    public const string Image = "fdcastel/firebird";

    /// <summary>3050</summary>
    public const ushort Port = 3050;

    /// <summary>ghcr.io</summary>
    public const string Registry = "ghcr.io";

    /// <summary>masterkey</summary>
    public const string SysDbaPassword = "masterkey";

    /// <summary>SYSDBA</summary>
    public const string SysDbaUsername = "SYSDBA";

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
            .WithRootPassword(SysDbaPassword)
            .WithUsername(DefaultUsername)
            .WithPassword(DefaultPassword)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(Port));

    /// <inheritdoc />
    protected override void Validate()
    {
        base.Validate();

        _ = Guard.Argument(DockerResourceConfiguration.Database, nameof(DockerResourceConfiguration.Database))
                .NotNull()
                .NotEmpty();

        // If a Firebird username is set, you must set a password. Otherwise, container initialization will fail.
        if (DockerResourceConfiguration.Username != null)
        {
            _ = Guard.Argument(DockerResourceConfiguration.Password, nameof(DockerResourceConfiguration.Password))
                .NotNull()
                .NotEmpty();
        }

        if (DockerResourceConfiguration.Password != null)
        {
            _ = Guard.Argument(DockerResourceConfiguration.Username, nameof(DockerResourceConfiguration.Username))
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
    public FirebirdBuilder WithConfig(string entry, string value)
        => Merge(DockerResourceConfiguration, new FirebirdConfiguration())
            .WithEnvironment("FIREBIRD_CONF_" + entry, value);

    /// <summary>
    ///  Sets the Firebird database name.
    /// </summary>
    /// <param name="database">The Firebird database name.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithDatabase(string database)
        => Merge(DockerResourceConfiguration, new FirebirdConfiguration(database: database))
            .WithEnvironment("FIREBIRD_DATABASE", database);

    /// <summary>
    ///  Sets the Firebird user password.
    /// </summary>
    /// <param name="password">The Firebird user password.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithPassword(string password)
        => Merge(DockerResourceConfiguration, new FirebirdConfiguration(password: password))
            .WithEnvironment("FIREBIRD_PASSWORD", password);

    /// <summary>
    ///  Sets the Firebird SYSDBA password.
    /// </summary>
    /// <param name="rootPassword">The Firebird SYSDBA password.</param>
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
    /// <param name="timezone">The Firebird container time zone.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithTimeZone(string timezone)
        => Merge(DockerResourceConfiguration, new FirebirdConfiguration())
            .WithEnvironment("TZ", timezone);

    /// <summary>
    ///  Enables legacy Firebird authentication (not recommended).
    /// </summary>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithUseLegacyAuth()
        => WithConfig("FIREBIRD_USE_LEGACY_AUTH", true.ToString());

    /// <summary>
    ///  Sets the Firebird user name.
    /// </summary>
    /// <param name="username">The Firebird username.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithUsername(string username)
        => Merge(DockerResourceConfiguration, new FirebirdConfiguration(username: username))
            .WithEnvironment("FIREBIRD_USER", username);
}