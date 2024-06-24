using BusinessObject.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.OData.ModelBuilder;

namespace eBookStoreWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ODataConventionModelBuilder builderO = new();
            builderO.EntitySet<Book>("Book");
            builderO.EntitySet<Author>("Author");
            builderO.EntitySet<BookAuthor>("BookAuthor");
            builderO.EntitySet<User>("User");
            builderO.EntitySet<Publisher>("Publisher");

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddOData(option => option.Select().Filter()
                .Count().OrderBy().Expand().SetMaxTop(100)
                .AddRouteComponents("odata", builderO.GetEdmModel()));

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

            app.UseODataBatching();

            app.Use(async (context, next) =>
            {
                Endpoint? endpoint = context.GetEndpoint();
                if (endpoint == null)
                {
                    await next();
                    return;
                }

                IODataRoutingMetadata? metaData = endpoint.Metadata.OfType<IODataRoutingMetadata>().FirstOrDefault();
                if (metaData != null)
                {
                    Microsoft.AspNetCore.OData.Routing.Template.ODataPathTemplate templates = metaData.Template;
                }

                await next();
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}