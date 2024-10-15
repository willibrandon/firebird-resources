using Firebird.Aspire.Client;
using FirebirdSql.Data.FirebirdClient;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.AddFirebirdClient("firebirdDb");

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapHealthChecks("/health");

app.MapGet("/getiscversion",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetIscVersionAsync();
    }
);

app.MapGet("/getserverversion",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetServerVersionAsync();
    }
);

app.MapGet("/getserverclass",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetServerClassAsync();
    }
);

app.MapGet("/getpagesize",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetPageSizeAsync();
    }
);

app.MapGet("/getallocationpages",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetAllocationPagesAsync();
    }
);

app.MapGet("/getbaselevel",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetBaseLevelAsync   ();
    }
);

app.MapGet("/getdbid",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetDbIdAsync();
    }
);

app.MapGet("/getimplementation",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetImplementationAsync();
    }
);

app.MapGet("/getnoreserve",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetNoReserveAsync();
    }
);

app.MapGet("/getodsversion",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetOdsVersionAsync();
    }
);

app.MapGet("/getodsminorversion",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetOdsMinorVersionAsync();
    }
);

app.MapGet("/getmaxmemory",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetMaxMemoryAsync();
    }
);

app.MapGet("/getcurrentmemory",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetCurrentMemoryAsync();
    }
);

app.MapGet("/getforcedwrites",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetForcedWritesAsync();
    }
);

app.MapGet("/getnumbuffers",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetNumBuffersAsync();
    }
);

app.MapGet("/getsweepinterval",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetSweepIntervalAsync();
    }
);

app.MapGet("/getreadonly",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetReadOnlyAsync();
    }
);

app.MapGet("/getfetches",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetFetchesAsync();
    }
);

app.MapGet("/getmarks",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetMarksAsync();
    }
);

app.MapGet("/getreads",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetReadsAsync();
    }
);

app.MapGet("/getwrites",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetWritesAsync();
    }
);

app.MapGet("/getbackoutcount",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetBackoutCountAsync();
    }
);

app.MapGet("/getdeletecount",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetDeleteCountAsync();
    }
);

app.MapGet("/getexpungecount",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetExpungeCountAsync();
    }
);

app.MapGet("/getinsertcount",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetInsertCountAsync();
    }
);

app.MapGet("/getpurgecount",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetPurgeCountAsync();
    }
);

app.MapGet("/getreadixcount",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetReadIdxCountAsync();
    }
);

app.MapGet("/getreadseqcount",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetReadSeqCountAsync();
    }
);

app.MapGet("/getupdatecount",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetUpdateCountAsync();
    }
);

app.MapGet("/getdatabasesizeinpages",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetDatabaseSizeInPagesAsync();
    }
);

app.MapGet("/getoldesttransaction",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetOldestTransactionAsync();
    }
);

app.MapGet("/getoldestactivetransaction",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetOldestActiveTransactionAsync();
    }
);

app.MapGet("/getoldestactivesnapshot",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetOldestActiveSnapshotAsync();
    }
);

app.MapGet("/getnexttransaction",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetNextTransactionAsync();
    }
);

app.MapGet("/getactivetransactions",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetActiveTransactionsAsync();
    }
);

app.MapGet("/getactivetransactionscount",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetActiveTransactionsCountAsync();
    }
);

app.MapGet("/getactiveusers",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetActiveUsersAsync();
    }
);

app.MapGet("/getwirecrypt",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetWireCryptAsync();
    }
);

app.MapGet("/getcryptplugin",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetCryptPluginAsync();
    }
);

app.MapGet("/getcreationdate",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetCreationDateAsync();
    }
);

app.MapGet("/getnextattachment",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetNextAttachmentAsync();
    }
);

app.MapGet("/getnextstatement",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetNextStatementAsync();
    }
);

app.MapGet("/getreplicamode",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetReplicaModeAsync();
    }
);

app.MapGet("/getdbfileid",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetDbFileIdAsync();
    }
);

app.MapGet("/getdbguid",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetDbGuidAsync();
    }
);

app.MapGet("/getcreationtimestamp",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetCreationTimestampAsync();
    }
);

app.MapGet("/getprotocolversion",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetProtocolVersionAsync();
    }
);

app.MapGet("/getstatementimeoutdatabase",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetStatementTimeoutDatabaseAsync();
    }
);

app.MapGet("/getstatementtimeoutattachment",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        var dbInfo = new FbDatabaseInfo(connection);
        return await dbInfo.GetStatementTimeoutAttachmentAsync();
    }
);

app.Run();
