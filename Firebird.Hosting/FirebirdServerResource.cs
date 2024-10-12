using Aspire.Hosting.ApplicationModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Firebird.Hosting;

/// <summary>
///  A resouce that represents a Firebird container.
/// </summary>
public class FirebirdServerResource : ContainerResource, IResourceWithConnectionString
{
    internal const string PrimaryEndpointName = "tcp";
    private const string DefaultUserName = "SYSDBA";

    private readonly Dictionary<string, string> _databases = new(StringComparer.OrdinalIgnoreCase);

    private ReferenceExpression ConnectionString =>
        ReferenceExpression.Create(
            $"Host={PrimaryEndpoint.Property(EndpointProperty.Host)};Port={PrimaryEndpoint.Property(EndpointProperty.Port)};Username={UserNameReference};Password={PasswordParameter}");

    // <summary>
    /// Gets the connection string expression for the Firebird server.
    /// </summary>
    public ReferenceExpression ConnectionStringExpression
    {
        get
        {
            if (this.TryGetLastAnnotation<ConnectionStringRedirectAnnotation>(out var connectionStringAnnotation))
            {
                return connectionStringAnnotation.Resource.ConnectionStringExpression;
            }

            return ConnectionString;
        }
    }

    /// <summary>
    /// A dictionary where the key is the resource name and the value is the database name.
    /// </summary>
    public IReadOnlyDictionary<string, string> Databases => _databases;

    /// <summary>
    /// Gets the primary endpoint for the Firebird server.
    /// </summary>
    public EndpointReference PrimaryEndpoint { get; }

    /// <summary>
    /// Gets the parameter that contains the Firebird server password.
    /// </summary>
    public ParameterResource PasswordParameter { get; }

    /// <summary>
    /// Gets or sets the parameter that contains the Firebird server user name.
    /// </summary>
    public ParameterResource? UserNameParameter { get; set; }

    internal ReferenceExpression UserNameReference =>
        UserNameParameter is not null ?
            ReferenceExpression.Create($"{UserNameParameter}") :
            ReferenceExpression.Create($"{DefaultUserName}");

    /// <summary>
    /// Initializes a new instance of the <see cref="FirebirdServerResource"/> class.
    /// </summary>
    /// <param name="name">The name of the resource.</param>
    /// <param name="userName">A parameter that contains the Firebird server user name, or <see langword="null"/> to use a default value.</param>
    /// <param name="password">A parameter that contains the Firebird server password.</param>
    public FirebirdServerResource(string name, ParameterResource? userName, ParameterResource password) : base(ThrowIfNull(name))
    {
        ArgumentNullException.ThrowIfNull(password);

        PrimaryEndpoint = new(this, PrimaryEndpointName);
        UserNameParameter = userName;
        PasswordParameter = password;
    }

    internal void AddDatabase(string name, string databaseName)
    {
        _databases.TryAdd(name, databaseName);
    }

    /// <summary>
    /// Gets the connection string for the Firebird.
    /// </summary>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A connection string for the Firebird in the form "Server=host,port;User ID=sa;Password=password;TrustServerCertificate=true".</returns>
    internal ValueTask<string?> GetConnectionStringAsync(CancellationToken cancellationToken = default)
    {
        if (this.TryGetLastAnnotation<ConnectionStringRedirectAnnotation>(out var connectionStringAnnotation))
        {
            return connectionStringAnnotation.Resource.GetConnectionStringAsync(cancellationToken);
        }

        return ConnectionString.GetValueAsync(cancellationToken);
    }

    private static string ThrowIfNull([NotNull] string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => argument ?? throw new ArgumentNullException(paramName);
}
