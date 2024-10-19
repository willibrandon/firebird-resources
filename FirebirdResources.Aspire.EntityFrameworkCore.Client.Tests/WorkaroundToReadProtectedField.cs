using Microsoft.EntityFrameworkCore;

namespace FirebirdResources.Aspire.EntityFrameworkCore.Client.Tests;

public class WorkaroundToReadProtectedField(DbContext context) : FirebirdRetryingExecutionStrategy(context)
{
    public int RetryCount => base.MaxRetryCount;
}