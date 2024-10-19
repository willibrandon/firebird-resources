using Firebird.Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var firebird = builder.AddFirebird("firebird")
    .AddDatabase("firebirdDb");

var apiService = builder.AddProject<Projects.FirebirdResource_ApiService>("apiservice")
    .WithReference(firebird);

builder.AddProject<Projects.FirebirdResource_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiService)
    .WithExternalHttpEndpoints();

builder.Build().Run();
