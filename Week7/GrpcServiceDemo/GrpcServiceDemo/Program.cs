using GrpcServiceDemo.DataAccess;
using GrpcServiceDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGrpc();
            builder.Services.AddDbContext<AppDbContext>(options =>
                            options.UseSqlServer(builder.Configuration.GetConnectionString("GrpcDb")));
            builder.Services.AddTransient<EmployeeCRUDService>();
            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<EmployeeCRUDService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}