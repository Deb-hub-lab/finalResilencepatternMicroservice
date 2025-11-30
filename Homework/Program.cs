
using Application.Services;
using Domain.Interfaces;
using HealthChecks.UI.Client;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Homework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
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


            // DbContext (generic connection string)
            var cs = builder.Configuration.GetConnectionString("Postgres");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(cs));



            // DI registrations
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IAttendanceService, AttendanceService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.MapHealthChecks("/health");

            app.MapControllers();
            // Map /health endpoint for this API
            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                // This formats the response for the HealthChecks UI
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.Run();
        }
    }
}
