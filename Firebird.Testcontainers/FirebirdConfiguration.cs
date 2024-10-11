namespace Firebird.Testcontainers;

/// <inheritdoc cref="ContainerConfiguration" />
[PublicAPI]
public sealed class FirebirdConfiguration : ContainerConfiguration
{
    private readonly string? _database;

    // /// <summary>
    // /// Gets the Firebird config.
    // /// </summary>
    // public object Config { get; }

    /// <summary>
    /// Gets the FirebirdSql database.
    /// </summary>
    public string? Database => _database;

    /// <summary>
    /// Gets the FirebirdSql password.
    /// </summary>
    public string? Password { get; }

    /// <summary>
    /// Gets the FirebirdSql username.
    /// </summary>
    public string? Username { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FirebirdConfiguration" /> class.
    /// </summary>
    public FirebirdConfiguration(string? database = null, string? password = null, string? username = null)
    {
        _database = database;
        Password = password;
        Username = username;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FirebirdConfiguration" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    public FirebirdConfiguration(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
        : base(resourceConfiguration)
    {
        // Passes the configuration upwards to the base implementations to create an updated immutable copy.
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FirebirdConfiguration" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    public FirebirdConfiguration(IContainerConfiguration resourceConfiguration)
        : base(resourceConfiguration)
    {
        // Passes the configuration upwards to the base implementations to create an updated immutable copy.
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FirebirdConfiguration" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    public FirebirdConfiguration(FirebirdConfiguration resourceConfiguration)
        : this(new FirebirdConfiguration(), resourceConfiguration)
    {
        // Passes the configuration upwards to the base implementations to create an updated immutable copy.
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FirebirdConfiguration" /> class.
    /// </summary>
    /// <param name="oldValue">The old Docker resource configuration.</param>
    /// <param name="newValue">The new Docker resource configuration.</param>
    public FirebirdConfiguration(FirebirdConfiguration oldValue, FirebirdConfiguration newValue)
        : base(oldValue, newValue)
    {
        _database = BuildConfiguration.Combine(oldValue._database, newValue._database);
        Password = BuildConfiguration.Combine(oldValue.Password, newValue.Password);
        Username = BuildConfiguration.Combine(oldValue.Username, newValue.Username);
    }
}