using Microsoft.EntityFrameworkCore;

namespace Firebird.Aspire.EntityFrameworkCore.Client.Tests;

public class WorkaroundToReadProtectedField(DbContext context) : FbRetryingExecutionStrategy(context)
{
    public int RetryCount => base.MaxRetryCount;
}