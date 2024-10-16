namespace FirebirdResource.Tests;

public class ApiServiceFbDatabaseInfoTests : IClassFixture<DistributedApplicationFixture>
{
    private readonly DistributedApplicationFixture _fixture;
    private readonly HttpClient _httpClient;

    public ApiServiceFbDatabaseInfoTests(DistributedApplicationFixture fixture)
    {
        _fixture = fixture;
        _httpClient = _fixture.App.CreateHttpClient("apiService");
    }

    [Fact]
    public async Task GetIscVersionReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getiscversion");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetServerVersionReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getserverversion");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetServerClassReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getserverclass");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetPageSizeReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getpagesize");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAllocationPagesReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getallocationpages");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetBaseLevelReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getbaselevel");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDbIdReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getdbid");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetImplementationReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getimplementation");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNoReserveReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getnoreserve");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOdsVersionReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getodsversion");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOdsMinorVersionReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getodsminorversion");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetMaxMemoryReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getmaxmemory");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCurrentMemoryReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getcurrentmemory");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetForcedWritesReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getforcedwrites");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNumBuffersReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getnumbuffers");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetSweepIntervalReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getsweepinterval");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReadOnlyReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getreadonly");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetFetchesReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getfetches");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetMarksReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getmarks");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReadsReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getreads");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetWritesReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getwrites");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetBackoutCountReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getbackoutcount");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDeleteCountReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getdeletecount");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetExpungeCountReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getexpungecount");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetInsertCountReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getinsertcount");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetPurgeCountReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getpurgecount");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReadIxCountReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getreadixcount");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReadSeqCountReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getreadseqcount");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetUpdateCountReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getupdatecount");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDatabaseSizeInPagesReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getdatabasesizeinpages");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOldestTransactionReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getoldesttransaction");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOldestActiveTransactionReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getoldestactivetransaction");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOldestActiveSnapshotReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getoldestactivesnapshot");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNextTransactionReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getnexttransaction");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetActiveTransactionReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getactivetransactions");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetActiveTransactionsCountReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getactivetransactionscount");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetActiveUsersReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getactiveusers");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetWireCryptReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getwirecrypt");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCryptPluginReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getcryptplugin");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCreationDateReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getcreationdate");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNextAttachmentReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getnextattachment");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNextStatementReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getnextstatement");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReplicaModeReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getreplicamode");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDbFileIdReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getdbfileid");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDbGuidtReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getdbguid");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCreationTimestampReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getcreationtimestamp");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetProtocolVersionReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getprotocolversion");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetStatementTimeoutDatabaseReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getstatementtimeoutdatabase");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetStatementTimeoutAttachmentReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbdatabaseinfo/getstatementtimeoutattachment");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
