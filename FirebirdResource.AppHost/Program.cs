using Firebird.Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.FirebirdResource_ApiService>("apiservice");

var firebird = builder.AddFirebird("firebird", "employees");

var firebirdDb = firebird.AddDatabase("firebirdDb", "employees");

builder.AddProject<Projects.FirebirdResource_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService)
    .WithReference(firebirdDb);

//builder.AddProject<Projects.FirebirdResource_ApiService>("apiservice")
//    .WithReference(firebirdDb);

builder.Build().Run();
