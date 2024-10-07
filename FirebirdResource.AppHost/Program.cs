using Aspire.Hosting.Firebird;

var builder = DistributedApplication.CreateBuilder(args);

var firebird = builder.AddFirebird("firebird");

builder.AddProject<Projects.FirebirdResource_ApiService>("apiservice")
    .WithReference(firebird);

builder.Build().Run();
