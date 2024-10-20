using FirebirdResources.Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var firebird = builder.AddFirebird("firebird")
    .AddDatabase("firebirdDb");

var apiService = builder.AddProject<Projects.FirebirdResource_ApiService>("apiservice")
    .WithReference(firebird).WaitFor(firebird);

builder.AddProject<Projects.FirebirdResource_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService).WaitFor(apiService)
    .WithReference(cache).WaitFor(cache);

builder.Build().Run();
