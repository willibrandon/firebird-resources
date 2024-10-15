using Firebird.Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var firebird = builder.AddFirebird("firebird")
    .AddDatabase("firebirdDb", "employees");

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.FirebirdResource_ApiService>("apiservice")
    .WithReference(firebird);

builder.AddProject<Projects.FirebirdResource_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
