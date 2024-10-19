using FirebirdResources.Aspire.Client;
using FirebirdResources.Aspire.EntityFrameworkCore.Client;
using FirebirdResource.ApiService;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddFirebirdClient("firebirdDb");
builder.AddFirebirdDbContext<CatalogDbContext>("firebirdDb");

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.MapCatalogApi();
app.MapFbDatabaseInfoApi();
app.MapFbTransactionInfoApi();
app.MapHealthChecks();

app.Run();
