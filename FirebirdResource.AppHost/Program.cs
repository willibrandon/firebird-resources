using Firebird.Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var firebird = builder.AddFirebird("firebird", "employees");
var firebirdDb = firebird.AddDatabase("firebirdDb", "employees");

builder.AddProject<Projects.FirebirdResource_ApiService>("apiservice")
    .WithReference(firebirdDb);

builder.Build().Run();
