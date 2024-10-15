namespace FirebirdResource.Tests;

public class FbDatabaseInfoTests: IClassFixture<DistributedApplicationFixture>
{
    private readonly DistributedApplicationFixture _fixture;
    private readonly HttpClient _httpClient;

    public FbDatabaseInfoTests(DistributedApplicationFixture fixture)
    {
        _fixture = fixture;
        _httpClient = _fixture.App.CreateHttpClient("apiService");
    }

    [Fact]
    public async Task GetHealthReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/health");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetIscVersionReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getiscversion");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetServerVersionReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getserverversion");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetServerClassReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getserverclass");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetPageSizeReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getpagesize");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAllocationPagesReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getallocationpages");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetBaseLevelReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getbaselevel");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDbIdReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getdbid");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetImplementationReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getimplementation");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNoReserveReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getnoreserve");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOdsVersionReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getodsversion");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOdsMinorVersionReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getodsminorversion");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetMaxMemoryReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getmaxmemory");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCurrentMemoryReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getcurrentmemory");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetForcedWritesReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getforcedwrites");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNumBuffersReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getnumbuffers");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetSweepIntervalReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getsweepinterval");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReadOnlyReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getreadonly");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetFetchesReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getfetches");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetMarksReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getmarks");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReadsReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getreads");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetWritesReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getwrites");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetBackoutCountReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getbackoutcount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDeleteCountReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getdeletecount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetExpungeCountReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getexpungecount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetInsertCountReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getinsertcount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetPurgeCountReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getpurgecount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReadIxCountReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getreadixcount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReadSeqCountReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getreadseqcount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetUpdateCountReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getupdatecount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDatabaseSizeInPagesReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getdatabasesizeinpages");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOldestTransactionReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getoldesttransaction");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOldestActiveTransactionReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getoldestactivetransaction");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOldestActiveSnapshotReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getoldestactivesnapshot");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNextTransactionReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getnexttransaction");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetActiveTransactionReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getactivetransactions");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetActiveTransactionsCountReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getactivetransactionscount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetActiveUsersReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getactiveusers");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetWireCryptReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getwirecrypt");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCryptPluginReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getcryptplugin");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCreationDateReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getcreationdate");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNextAttachmentReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getnextattachment");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNextStatementReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getnextstatement");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReplicaModeReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getreplicamode");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDbFileIdReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getdbfileid");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDbGuidtReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getdbguid");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCreationTimestampReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getcreationtimestamp");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetProtocolVersionReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getprotocolversion");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetStatementTimeoutDatabaseReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getstatementtimeoutdatabase");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetStatementTimeoutAttachmentReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/getstatementtimeoutattachment");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
