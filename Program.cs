using FluentValidation;
using DoctorService.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DoctorService.Mappings;
using MedicalHealthPlus.Interfaces;

namespace DoctorService;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.RegisterMapsterConfiguration();
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        var defName = builder.Configuration["Db:Name"];
        var defHost = builder.Configuration["Db:Host"];
        var defPass = builder.Configuration["Db:Pass"];
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? defHost;
        var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? defName;
        var dbPass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? defPass;

        string? connectionString = $"Server={dbHost}; Persist Security Info=False; TrustServerCertificate=true; User ID=sa;Password={dbPass};Initial Catalog={dbName};";
        builder.Services.AddDbContext<DoctorServiceContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
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

