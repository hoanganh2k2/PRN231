using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataASPNetCoreDemo.Data.Entities;

namespace ODataASPNetCoreDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<MyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Week3Demo"));
            });

            builder.Services.AddControllers().AddOData(options => options.Select().Filter().Count().OrderBy().Expand()
            .SetMaxTop(100)
            .AddRouteComponents("odata", GetEdmModel()));
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
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new();
            modelBuilder.EntitySet<Gadgets>("GadgetsOdata");
            return modelBuilder.GetEdmModel();
        }
    }
}