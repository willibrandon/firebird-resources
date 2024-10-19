using FirebirdSql.Data.FirebirdClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FirebirdResources.Aspire.EntityFrameworkCore.Client;

public class FirebirdRetryingExecutionStrategy : ExecutionStrategy
{
    private readonly ICollection<string>? _additionalErrorCodes;

    public FirebirdRetryingExecutionStrategy(
        DbContext context)
        : this(context, DefaultMaxRetryCount)
    {
    }

    public FirebirdRetryingExecutionStrategy(
        ExecutionStrategyDependencies dependencies)
        : this(dependencies, DefaultMaxRetryCount)
    {
    }

    public FirebirdRetryingExecutionStrategy(
        DbContext context,
        int maxRetryCount)
        : this(context, maxRetryCount, DefaultMaxDelay, errorCodesToAdd: null)
    {
    }

    public FirebirdRetryingExecutionStrategy(
        ExecutionStrategyDependencies dependencies,
        int maxRetryCount)
        : this(dependencies, maxRetryCount, DefaultMaxDelay, errorCodesToAdd: null)
    {
    }

    public FirebirdRetryingExecutionStrategy(
        ExecutionStrategyDependencies dependencies,
        ICollection<string>? errorCodesToAdd)
        : this(dependencies, DefaultMaxRetryCount, DefaultMaxDelay, errorCodesToAdd)
    {
    }

    public FirebirdRetryingExecutionStrategy(
        DbContext context,
        int maxRetryCount,
        TimeSpan maxRetryDelay,
        ICollection<string>? errorCodesToAdd)
        : base(
            context,
            maxRetryCount,
            maxRetryDelay)
    {
        _additionalErrorCodes = errorCodesToAdd;
    }

    public FirebirdRetryingExecutionStrategy(
        ExecutionStrategyDependencies dependencies,
        int maxRetryCount,
        TimeSpan maxRetryDelay,
        ICollection<string>? errorCodesToAdd)
        : base(dependencies, maxRetryCount, maxRetryDelay)
    {
        _additionalErrorCodes = errorCodesToAdd;
    }

    /// <inheritdoc />
    protected override bool ShouldRetryOn(Exception exception)
        => exception is FbException fbException
            && fbException.SqlState != null
            && _additionalErrorCodes?.Contains(fbException.SqlState) == true;
}
