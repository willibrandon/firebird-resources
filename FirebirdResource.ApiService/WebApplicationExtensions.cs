using Firebird.Aspire.Client;
using FirebirdSql.Data.FirebirdClient;

namespace FirebirdResource.ApiService;

public static class WebApplicationExtensions
{
    public static void MapApiService(this WebApplication app)
    {
        app.MapHealthChecks();
        app.MapFbDatabaseInfo();
        app.MapFbTransactionInfo();
    }

    public static void MapFbDatabaseInfo(this WebApplication app)
    {
        app.MapGet("/fbdatabaseinfo/getiscversion",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetIscVersionAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getserverversion",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetServerVersionAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getserverclass",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetServerClassAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getpagesize",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetPageSizeAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getallocationpages",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetAllocationPagesAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getbaselevel",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetBaseLevelAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getdbid",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetDbIdAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getimplementation",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetImplementationAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getnoreserve",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetNoReserveAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getodsversion",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetOdsVersionAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getodsminorversion",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetOdsMinorVersionAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getmaxmemory",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetMaxMemoryAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getcurrentmemory",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetCurrentMemoryAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getforcedwrites",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetForcedWritesAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getnumbuffers",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetNumBuffersAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getsweepinterval",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetSweepIntervalAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getreadonly",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetReadOnlyAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getfetches",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetFetchesAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getmarks",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetMarksAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getreads",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetReadsAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getwrites",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetWritesAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getbackoutcount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetBackoutCountAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getdeletecount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetDeleteCountAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getexpungecount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetExpungeCountAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getinsertcount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetInsertCountAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getpurgecount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetPurgeCountAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getreadixcount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetReadIdxCountAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getreadseqcount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetReadSeqCountAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getupdatecount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetUpdateCountAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getdatabasesizeinpages",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetDatabaseSizeInPagesAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getoldesttransaction",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetOldestTransactionAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getoldestactivetransaction",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetOldestActiveTransactionAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getoldestactivesnapshot",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetOldestActiveSnapshotAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getnexttransaction",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetNextTransactionAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getactivetransactions",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetActiveTransactionsAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getactivetransactionscount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetActiveTransactionsCountAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getactiveusers",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetActiveUsersAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getwirecrypt",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetWireCryptAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getcryptplugin",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetCryptPluginAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getcreationdate",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetCreationDateAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getnextattachment",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetNextAttachmentAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getnextstatement",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetNextStatementAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getreplicamode",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetReplicaModeAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getdbfileid",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetDbFileIdAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getdbguid",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetDbGuidAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getcreationtimestamp",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetCreationTimestampAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getprotocolversion",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetProtocolVersionAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getstatementtimeoutdatabase",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetStatementTimeoutDatabaseAsync();
            }
        );

        app.MapGet("/fbdatabaseinfo/getstatementtimeoutattachment",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetStatementTimeoutAttachmentAsync();
            }
        );
    }

    public static void MapFbTransactionInfo(this WebApplication app)
    {
        app.MapGet("/fbtransactioninfo/gettransactionsnapshotnumber",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                using var transaction = connection.BeginTransaction();
                var txInfo = new FbTransactionInfo(transaction);
                return await txInfo.GetTransactionSnapshotNumberAsync();
            }
        );
    }

    public static void MapHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health");
    }
}
