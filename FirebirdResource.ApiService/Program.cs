using Firebird.Aspire.Client;
using Firebird.Aspire.EntityFrameworkCore.Client;
using FirebirdResource.ApiService;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.

builder.AddFirebirdClient("firebirdDb");
builder.AddFirebirdDbContext<FirebirdtDbContext>("firebirdDb");

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapApiService();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<FirebirdtDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.Run();
