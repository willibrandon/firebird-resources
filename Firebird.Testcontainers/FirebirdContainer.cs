namespace Firebird.Testcontainers;

/// <inheritdoc cref="DockerContainer" />
[PublicAPI]
public sealed class FirebirdContainer : DockerContainer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FirebirdContainer" /> class.
    /// </summary>
    /// <param name="configuration">The container configuration.</param>
    public FirebirdContainer(FirebirdConfiguration configuration)
        : base(configuration)
    {
    }
}