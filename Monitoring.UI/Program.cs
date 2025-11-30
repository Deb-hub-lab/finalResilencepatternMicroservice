using HealthChecks.UI.Client;
using HealthChecks.Uris;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var servicesToMonitor = new List<(string Name, string Url)>
{
    ("Attendance API", "https://localhost:7232/health"),
    ("Student API", "https://localhost:7192/health"),
    ("Grade API", "https://localhost:7290/health")
};

// Add HealthChecks dynamically
var hc = builder.Services.AddHealthChecks();
foreach (var s in servicesToMonitor)
{
    hc.AddUrlGroup(new Uri(s.Url), s.Name, HealthStatus.Unhealthy);
}

// Add UI endpoints dynamically
builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(10);

    foreach (var s in servicesToMonitor)
        options.AddHealthCheckEndpoint(s.Name, s.Url);
})
.AddInMemoryStorage();

// -------------------------------
// 1. Add HealthChecks (this monitors your API URL)
// -------------------------------
//builder.Services.AddHealthChecks()
//    .AddUrlGroup(new Uri("https://localhost:7232/health"),name: "Attendance API",failureStatus: HealthStatus.Unhealthy)
//    .AddUrlGroup(new Uri("https://localhost:7240/health"), name: "Student API", failureStatus: HealthStatus.Unhealthy)
//    .AddUrlGroup(new Uri("https://localhost:7250/health"), name: "Billing API", failureStatus: HealthStatus.Unhealthy);

// -------------------------------
// 2. Configure HealthChecks UI
// -------------------------------
//builder.Services.AddHealthChecksUI(options =>
//{
//    options.SetEvaluationTimeInSeconds(10);
//    options.MaximumHistoryEntriesPerEndpoint(50);

//    // IMPORTANT — Add monitored endpoints for UI
//    options.AddHealthCheckEndpoint(name: "Attendance API",uri: "https://localhost:7232/health");

//})
//.AddInMemoryStorage();

var app = builder.Build();
app.UseStaticFiles();

// -------------------------------
// 3. Map endpoints
// -------------------------------
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";
    options.ApiPath = "/health-ui-api";
    options.AddCustomStylesheet("wwwroot/css/custom.css");
});

app.Run();
