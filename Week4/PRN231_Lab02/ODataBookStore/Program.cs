using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataBookStore.Models;
using System.Text.Json;

namespace ODataBookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BookStoreContext>(opt => opt.UseInMemoryDatabase("BookLists"));

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                });

            builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel())
            .Select()
            .Expand()
            .Filter()
            .OrderBy()
            .Count()
            .SetMaxTop(100));

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

            app.UseODataBatching();
            app.UseRouting();

            // Test middleware
            app.Use((context, next) =>
            {
                Endpoint? endpoint = context.GetEndpoint();
                if (endpoint == null)
                    return next(context);

                IEnumerable<string> templates;

                IODataRoutingMetadata? metadata = endpoint.Metadata.GetMetadata<IODataRoutingMetadata>();
                if (metadata != null)
                    templates = metadata.Template.GetTemplates();

                return next(context);
            });

            app.MapControllers();

            app.Run();
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new();
            builder.EntitySet<Book>("Books");
            builder.EntitySet<Press>("Presses");
            return builder.GetEdmModel();
        }
    }
}