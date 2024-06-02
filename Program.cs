using FluentValidation;
using DoctorService.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DoctorService.Mappings;

namespace DoctorService;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.RegisterMapsterConfiguration();
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        string? defName = builder.Configuration["Db:Name"];
        string? defHost = builder.Configuration["Db:Host"];
        string? defPass = builder.Configuration["Db:Pass"];
        string? dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? defHost;
        string? dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? defName;
        string? dbPass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? defPass;
        string? connectionString = $"Server={dbHost}; Persist Security Info=False; TrustServerCertificate=true; User ID=sa;Password={dbPass};Initial Catalog={dbName};";
        builder.Services.AddDbContext<DoctorServiceContext>(options => options.UseSqlServer(connectionString));

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}

