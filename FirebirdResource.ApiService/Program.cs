using Firebird.Client;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.AddFirebirdClient("firebird");

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGet("/health",
    async (FbConnectionFactory factory) =>
    {
        const string checkHealthQuery = "SELECT 1 FROM RDB$DATABASE;";
        using FbConnection connection = await factory.GetFbConnectionAsync();
        using var transaction = connection.BeginTransaction();
        using var command = new FbCommand(checkHealthQuery, connection, transaction);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);
            Console.WriteLine(string.Join("|", values));

            int value = reader.GetInt32(0);
            return value == 1 ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
        }

        return HealthCheckResult.Unhealthy();
    }
);

var summaries = new[]
{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
