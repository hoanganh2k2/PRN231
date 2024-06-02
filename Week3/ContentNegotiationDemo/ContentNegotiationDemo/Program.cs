using ContentNegotiationDemo.CustomFormatters;

namespace ContentNegotiationDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(config =>
            {
                // Respect the Accept header from the browser/client
                config.RespectBrowserAcceptHeader = true;
                // Return 406 Not Acceptable if the requested format is not supported
                config.ReturnHttpNotAcceptable = true;
            })
            // Add XML support
            .AddXmlDataContractSerializerFormatters()
            // Add custom CSV formatter
            .AddMvcOptions(options =>
            {
                options.OutputFormatters.Add(new CsvOutputFormatter());
            });

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
}
