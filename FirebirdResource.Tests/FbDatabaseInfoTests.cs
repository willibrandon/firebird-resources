namespace FirebirdResource.Tests;

public class FbDatabaseInfoTests(DistributedApplicationFixture fixture) : IClassFixture<DistributedApplicationFixture>
{
    private readonly DistributedApplicationFixture _fixture = fixture;

    [Fact]
    public async Task GetHealthReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/health");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetIscVersionReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getiscversion");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetServerVersionReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getserverversion");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetServerClassReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getserverclass");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetPageSizeReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getpagesize");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAllocationPagesReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getallocationpages");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetBaseLevelReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getbaselevel");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDbIdReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getdbid");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetImplementationReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getimplementation");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNoReserveReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getnoreserve");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOdsVersionReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getodsversion");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOdsMinorVersionReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getodsminorversion");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetMaxMemoryReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getmaxmemory");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCurrentMemoryReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getcurrentmemory");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetForcedWritesReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getforcedwrites");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNumBuffersReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getnumbuffers");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetSweepIntervalReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getsweepinterval");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReadOnlyReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getreadonly");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetFetchesReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getfetches");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetMarksReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getmarks");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReadsReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getreads");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetWritesReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getwrites");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetBackoutCountReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getbackoutcount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDeleteCountReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getdeletecount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetExpungeCountReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getexpungecount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetInsertCountReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getinsertcount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetPurgeCountReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getpurgecount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReadIxCountReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getreadixcount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReadSeqCountReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getreadseqcount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetUpdateCountReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getupdatecount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDatabaseSizeInPagesReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getdatabasesizeinpages");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOldestTransactionReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getoldesttransaction");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOldestActiveTransactionReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getoldestactivetransaction");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOldestActiveSnapshotReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getoldestactivesnapshot");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNextTransactionReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getnexttransaction");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetActiveTransactionReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getactivetransactions");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetActiveTransactionsCountReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getactivetransactionscount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetActiveUsersReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getactiveusers");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetWireCryptReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getwirecrypt");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCryptPluginReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getcryptplugin");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCreationDateReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getcreationdate");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNextAttachmentReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getnextattachment");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNextStatementReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getnextstatement");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReplicaModeReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getreplicamode");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDbFileIdReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getdbfileid");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDbGuidtReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getdbguid");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCreationTimestampReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getcreationtimestamp");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetProtocolVersionReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getprotocolversion");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetStatementTimeoutDatabaseReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getstatementtimeoutdatabase");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetStatementTimeoutAttachmentReturnsOkStatusCode()
    {
        // Arrange
        Assert.NotNull(_fixture.App);

        // Act
        var httpClient = _fixture.App.CreateHttpClient("apiService");
        var response = await httpClient.GetAsync("/getstatementtimeoutattachment");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
