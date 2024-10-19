using FirebirdSql.Data.FirebirdClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Firebird.Aspire.EntityFrameworkCore.Client;

public class FbRetryingExecutionStrategy : ExecutionStrategy
{
    private readonly ICollection<string>? _additionalErrorCodes;

    public FbRetryingExecutionStrategy(
        DbContext context)
        : this(context, DefaultMaxRetryCount)
    {
    }

    public FbRetryingExecutionStrategy(
        ExecutionStrategyDependencies dependencies)
        : this(dependencies, DefaultMaxRetryCount)
    {
    }

    public FbRetryingExecutionStrategy(
        DbContext context,
        int maxRetryCount)
        : this(context, maxRetryCount, DefaultMaxDelay, errorCodesToAdd: null)
    {
    }

    public FbRetryingExecutionStrategy(
        ExecutionStrategyDependencies dependencies,
        int maxRetryCount)
        : this(dependencies, maxRetryCount, DefaultMaxDelay, errorCodesToAdd: null)
    {
    }

    public FbRetryingExecutionStrategy(
        ExecutionStrategyDependencies dependencies,
        ICollection<string>? errorCodesToAdd)
        : this(dependencies, DefaultMaxRetryCount, DefaultMaxDelay, errorCodesToAdd)
    {
    }

    public FbRetryingExecutionStrategy(
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

    public FbRetryingExecutionStrategy(
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
