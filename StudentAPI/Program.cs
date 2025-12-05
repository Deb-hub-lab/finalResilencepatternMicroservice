using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Polly;
using Polly.Extensions.Http;
using StudentAPI.Middleware;
using StudentAPI.Repositories;
using StudentAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddHttpClient<IGradeService, GradeService>(options =>
//{
//    options.BaseAddress = new Uri(builder.Configuration["GradeApiConfig:BaseUrl"]);
//});

builder.Services.AddGradeServicesToApiContainer(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddMemoryCache(); // enable in-memory caching

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddSingleton<CacheTokenProvider>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
            .AddCheck("Database", () =>
            {
                // You can put real DB check logic here
                bool dbIsHealthy = true;

                if (dbIsHealthy)
                    return HealthCheckResult.Healthy("Database is OK");
                else
                    return HealthCheckResult.Unhealthy("Database check failed");
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapControllers();
// Map /health endpoint for this API
app.MapHealthChecks("/health", new HealthCheckOptions
{
    // This formats the response for the HealthChecks UI
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();
