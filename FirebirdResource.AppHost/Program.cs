using Firebird.Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var firebird = builder.AddFirebird("firebird")
    .AddDatabase("firebirdDb");

var apiService = builder.AddProject<Projects.FirebirdResource_ApiService>("apiservice")
    .WithReference(firebird);

var cache = builder.AddRedis("cache");

builder.AddProject<Projects.FirebirdResource_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
