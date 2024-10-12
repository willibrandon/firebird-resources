using Firebird.Client;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.AddFirebirdClient("firebirdDb");

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGet("/health",
    async (FbConnectionFactory factory) =>
    {
        using FbConnection connection = await factory.GetFbConnectionAsync();
        using var transaction = connection.BeginTransaction();
        using var command = new FbCommand("SELECT 1 FROM RDB$DATABASE;", connection, transaction);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            return reader.GetInt32(0) switch
            {
                1 => HealthCheckResult.Healthy(),
                _ => HealthCheckResult.Unhealthy()
            };
        }

        return HealthCheckResult.Unhealthy();
    }
);

app.Run();
