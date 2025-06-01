
using FlamboyantFnb.Extensions;
using FlamboyantFnb.Helper;
using Serilog;

namespace FlamboyantFnb.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                                   //.WriteTo.Seq("http://localhost:5341") // Optional: send to Seq
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .CreateLogger();
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog();
            AppServiceConfig.Initialize(builder.Configuration);
            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                // Disable camelCase to use PascalCase
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors((options) =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins(AppServiceConfig.FrontEndOrigin)
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .AllowAnyHeader();
                });
            });

            builder.LoadSecretKey();
            builder.ConfigServices();
            builder.AddRedis();
            builder.AddContext();

            var app = builder.Build();
            app.UseExceptionHandler(builder =>
            {
                builder.UseGlobalExceptionProcess();
            });
            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
