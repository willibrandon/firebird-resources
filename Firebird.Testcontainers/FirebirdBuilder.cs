using System.Text;

namespace Firebird.Testcontainers;

/// <inheritdoc cref="ContainerBuilder{TBuilderEntity, TContainerEntity, TConfigurationEntity}" />
[PublicAPI]
public sealed class FirebirdBuilder : ContainerBuilder<FirebirdBuilder, FirebirdContainer, FirebirdConfiguration>
{
    private const string DefaultDatabase = "myDb";
    private const string DefaultRootPassword = "masterkey";
    private const string TestQueryString = "SELECT 1 FROM RDB$DATABASE;";

    /// <summary>/var/lib/firebird/data/</summary>
    public const string DefaultDatabaseLocation = "/var/lib/firebird/data/";

    /// <summary>3050</summary>
    public const ushort FirebirdSqlPort = 3050;

    /// <summary>fdcastel/firebird</summary>
    public const string Image = "fdcastel/firebird";

    /// <summary>docker.io</summary>
    public const string Registry = "ghcr.io";

    /// <summary>SYSDBA</summary>
    public const string SysDbaUsername = "SYSDBA";

    /// <summary>latest</summary>
    public const string Tag = "latest";

    /// <inheritdoc />
    protected override FirebirdConfiguration DockerResourceConfiguration { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FirebirdBuilder" /> class.
    /// </summary>
    public FirebirdBuilder()
        : this(new FirebirdConfiguration())
    {
        // 1) To change the ContainerBuilder default configuration override the DockerResourceConfiguration property and the "FirebirdBuilder Init()" method.
        //    Append the module configuration to base.Init() e.g. base.Init().WithImage("alpine:3.17") to set the modules' default image.

        // 2) To customize the ContainerBuilder validation override the "void Validate()" method.
        //    Use Testcontainers' Guard.Argument<TType>(TType, string) or your own guard implementation to validate the module configuration.

        // 3) Add custom builder methods to extend the ContainerBuilder capabilities such as "FirebirdBuilder WithFirebirdConfig(object)".
        //    Merge the current module configuration with a new instance of the immutable FirebirdConfiguration type to update the module configuration.

        DockerResourceConfiguration = Init().DockerResourceConfiguration;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FirebirdBuilder" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    private FirebirdBuilder(FirebirdConfiguration resourceConfiguration)
        : base(resourceConfiguration)
    {
        DockerResourceConfiguration = resourceConfiguration;
    }

    /// <summary>
    /// Sets the Firebird config.
    /// </summary>
    /// <param name="config">The Firebird config.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithFirebirdConfig(object config)
    {
        // Extends the ContainerBuilder capabilities and holds a custom configuration in FirebirdConfiguration.
        // In case of a module requires other properties to represent itself, extend ContainerConfiguration.
        return Merge(
            DockerResourceConfiguration,
            new FirebirdConfiguration(database: DefaultDatabase, username: null, password: null));
    }

    /// <summary>
    /// Sets the FirebirdSql database.
    /// </summary>
    /// <param name="database">The FirebirdSql database.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithDatabase(string database)
    {
        return Merge(DockerResourceConfiguration, new FirebirdConfiguration(database: database))
            .WithEnvironment("FIREBIRD_DATABASE", database);
    }

    /// <summary>
    /// Sets the FirebirdSql password.
    /// </summary>
    /// <param name="password">The FirebirdSql password.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithPassword(string password)
    {
        return Merge(DockerResourceConfiguration, new FirebirdConfiguration(password: password))
            .WithEnvironment("FIREBIRD_PASSWORD", password);
    }

    /// <summary>
    /// Sets the Firebird root password.
    /// </summary>
    /// <param name="password">The FirebirdSql password.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithRootPassword(string password)
    {
        return Merge(DockerResourceConfiguration, new FirebirdConfiguration(password: password))
            .WithEnvironment("FIREBIRD_ROOT_PASSWORD", password);
    }

    /// <summary>
    /// Sets the FirebirdSql username.
    /// </summary>
    /// <param name="username">The FirebirdSql username.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithUsername(string username)
    {
        return Merge(DockerResourceConfiguration, new FirebirdConfiguration(username: username))
            .WithEnvironment("FIREBIRD_USER", SysDbaUsername.Equals(username, StringComparison.OrdinalIgnoreCase) ? string.Empty : username);
    }

    /// <inheritdoc />
    public override FirebirdContainer Build()
    {
        Validate();
        return new FirebirdContainer(DockerResourceConfiguration);
    }

    /// <inheritdoc />
    protected override FirebirdBuilder Init()
    {
        return base.Init()
            .WithImage(Registry + "/" + Image + ":" + Tag)
            .WithPortBinding(FirebirdSqlPort, true)
            .WithDatabase(DefaultDatabase)
            .WithUsername(SysDbaUsername)
            .WithPassword(DefaultRootPassword)
            .WithRootPassword(DefaultRootPassword)
            .WithResourceMapping(Encoding.Default.GetBytes(TestQueryString), "/home/firebird_check.sql");
            //.WithWaitStrategy(Wait.ForUnixContainer().UntilContainerIsHealthy());
    }

    /// <inheritdoc />
    protected override void Validate()
    {
        base.Validate();

        _ = Guard.Argument(DockerResourceConfiguration.Password, nameof(DockerResourceConfiguration.Password))
            .NotNull()
            .NotEmpty();
    }

    /// <inheritdoc />
    protected override FirebirdBuilder Clone(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
    {
        return Merge(DockerResourceConfiguration, new FirebirdConfiguration(resourceConfiguration));
    }

    /// <inheritdoc />
    protected override FirebirdBuilder Clone(IContainerConfiguration resourceConfiguration)
    {
        return Merge(DockerResourceConfiguration, new FirebirdConfiguration(resourceConfiguration));
    }

    /// <inheritdoc />
    protected override FirebirdBuilder Merge(FirebirdConfiguration oldValue, FirebirdConfiguration newValue)
    {
        return new FirebirdBuilder(new FirebirdConfiguration(oldValue, newValue));
    }
}