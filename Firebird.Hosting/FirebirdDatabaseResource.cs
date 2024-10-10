using Aspire.Hosting.ApplicationModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Firebird.Hosting;

/// <summary>
/// A resource that represents a Firebird SQL database. This is a child resource of a <see cref="FirebirdServerResource"/>.
/// </summary>
/// <param name="name">The name of the resource.</param>
/// <param name="databaseName">The database name.</param>
/// <param name="firebirdParentResource">The Firebird SQL parent resource associated with this database.</param>
public class FirebirdDatabaseResource(string name, string databaseName, FirebirdServerResource firebirdParentResource) : Resource(ThrowIfNull(name)), IResourceWithParent<FirebirdServerResource>, IResourceWithConnectionString
{
    /// <summary>
    /// Gets the parent Firebird SQL container resource.
    /// </summary>
    public FirebirdServerResource Parent { get; } = ThrowIfNull(firebirdParentResource);

    /// <summary>
    /// Gets the connection string expression for the Firebird database.
    /// </summary>
    public ReferenceExpression ConnectionStringExpression =>
       ReferenceExpression.Create($"{Parent};Database={DatabaseName}");

    /// <summary>
    /// Gets the database name.
    /// </summary>
    public string DatabaseName { get; } = ThrowIfNull(databaseName);

    private static T ThrowIfNull<T>([NotNull] T? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => argument ?? throw new ArgumentNullException(paramName);
}

