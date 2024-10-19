using Firebird.Aspire.Client;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.EntityFrameworkCore;

namespace FirebirdResource.ApiService;

public static class ApiEndpoints
{
    public static WebApplication MapCatalogApi(this WebApplication app)
    {
        var group = app.MapGroup("/catalog");

        group.MapGet("brands", async (CatalogDbContext dbContext) =>
            await dbContext.CatalogBrands.ToListAsync());

        group.MapGet("brands/{id}", async (int id, CatalogDbContext dbContext) =>
            await dbContext.CatalogBrands.FindAsync(id) is CatalogBrand catalogBrand
                ? Results.Ok(catalogBrand)
                : Results.NotFound());

        group.MapPost("brands", async (CatalogBrand catalogBrand, CatalogDbContext dbContext) =>
        {
            dbContext.CatalogBrands.Add(catalogBrand);
            await dbContext.SaveChangesAsync();
            return Results.Created($"/brands/{catalogBrand.Id}", catalogBrand);
        });

        group.MapPut("brands/{id}", async (int id, CatalogBrand catalogBrand, CatalogDbContext dbContext) =>
        {
            var brand = await dbContext.CatalogBrands.FindAsync(id);
            if (brand is null) return Results.NotFound();

            brand.Brand = catalogBrand.Brand;
            await dbContext.SaveChangesAsync();
            return Results.Ok(brand);
        });

        group.MapDelete("brands/{id}", async (int id, CatalogDbContext dbContext) =>
        {
            if (await dbContext.CatalogBrands.FindAsync(id) is CatalogBrand catalogBrand)
            {
                dbContext.CatalogBrands.Remove(catalogBrand);
                await dbContext.SaveChangesAsync();
                return Results.Ok($"{id} deleted");
            }

            return Results.NotFound();
        });

        return app;
    }

    public static WebApplication MapFbDatabaseInfoApi(this WebApplication app)
    {
        var group = app.MapGroup("/fbdatabaseinfo");

        group.MapGet("getiscversion",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetIscVersionAsync();
            }
        );

        group.MapGet("getserverversion",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetServerVersionAsync();
            }
        );

        group.MapGet("getserverclass",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetServerClassAsync();
            }
        );

        group.MapGet("getpagesize",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetPageSizeAsync();
            }
        );

        group.MapGet("getallocationpages",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetAllocationPagesAsync();
            }
        );

        group.MapGet("getbaselevel",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetBaseLevelAsync();
            }
        );

        group.MapGet("getdbid",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetDbIdAsync();
            }
        );

        group.MapGet("getimplementation",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetImplementationAsync();
            }
        );

        group.MapGet("getnoreserve",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetNoReserveAsync();
            }
        );

        group.MapGet("getodsversion",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetOdsVersionAsync();
            }
        );

        group.MapGet("getodsminorversion",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetOdsMinorVersionAsync();
            }
        );

        group.MapGet("getmaxmemory",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetMaxMemoryAsync();
            }
        );

        group.MapGet("getcurrentmemory",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetCurrentMemoryAsync();
            }
        );

        group.MapGet("getforcedwrites",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetForcedWritesAsync();
            }
        );

        group.MapGet("getnumbuffers",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetNumBuffersAsync();
            }
        );

        group.MapGet("getsweepinterval",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetSweepIntervalAsync();
            }
        );

        group.MapGet("getreadonly",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetReadOnlyAsync();
            }
        );

        group.MapGet("getfetches",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetFetchesAsync();
            }
        );

        group.MapGet("getmarks",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetMarksAsync();
            }
        );

        group.MapGet("getreads",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetReadsAsync();
            }
        );

        group.MapGet("getwrites",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetWritesAsync();
            }
        );

        group.MapGet("getbackoutcount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetBackoutCountAsync();
            }
        );

        group.MapGet("getdeletecount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetDeleteCountAsync();
            }
        );

        group.MapGet("getexpungecount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetExpungeCountAsync();
            }
        );

        group.MapGet("getinsertcount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetInsertCountAsync();
            }
        );

        group.MapGet("getpurgecount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetPurgeCountAsync();
            }
        );

        group.MapGet("getreadixcount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetReadIdxCountAsync();
            }
        );

        group.MapGet("getreadseqcount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetReadSeqCountAsync();
            }
        );

        group.MapGet("getupdatecount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetUpdateCountAsync();
            }
        );

        group.MapGet("getdatabasesizeinpages",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetDatabaseSizeInPagesAsync();
            }
        );

        group.MapGet("getoldesttransaction",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetOldestTransactionAsync();
            }
        );

        group.MapGet("getoldestactivetransaction",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetOldestActiveTransactionAsync();
            }
        );

        group.MapGet("getoldestactivesnapshot",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetOldestActiveSnapshotAsync();
            }
        );

        group.MapGet("getnexttransaction",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetNextTransactionAsync();
            }
        );

        group.MapGet("getactivetransactions",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetActiveTransactionsAsync();
            }
        );

        group.MapGet("getactivetransactionscount",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetActiveTransactionsCountAsync();
            }
        );

        group.MapGet("getactiveusers",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetActiveUsersAsync();
            }
        );

        group.MapGet("getwirecrypt",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetWireCryptAsync();
            }
        );

        group.MapGet("getcryptplugin",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetCryptPluginAsync();
            }
        );

        group.MapGet("getcreationdate",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetCreationDateAsync();
            }
        );

        group.MapGet("getnextattachment",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetNextAttachmentAsync();
            }
        );

        group.MapGet("getnextstatement",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetNextStatementAsync();
            }
        );

        group.MapGet("getreplicamode",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetReplicaModeAsync();
            }
        );

        group.MapGet("getdbfileid",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetDbFileIdAsync();
            }
        );

        group.MapGet("getdbguid",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetDbGuidAsync();
            }
        );

        group.MapGet("getcreationtimestamp",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetCreationTimestampAsync();
            }
        );

        group.MapGet("getprotocolversion",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetProtocolVersionAsync();
            }
        );

        group.MapGet("getstatementtimeoutdatabase",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetStatementTimeoutDatabaseAsync();
            }
        );

        group.MapGet("getstatementtimeoutattachment",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                var dbInfo = new FbDatabaseInfo(connection);
                return await dbInfo.GetStatementTimeoutAttachmentAsync();
            }
        );

        return app;
    }

    public static WebApplication MapFbTransactionInfoApi(this WebApplication app)
    {
        var group = app.MapGroup("fbtransactioninfo");

        group.MapGet("gettransactionsnapshotnumber",
            async (FbConnectionFactory factory) =>
            {
                using FbConnection connection = await factory.GetFbConnectionAsync();
                using var transaction = connection.BeginTransaction();
                var txInfo = new FbTransactionInfo(transaction);
                return await txInfo.GetTransactionSnapshotNumberAsync();
            }
        );

        return app;
    }
}
