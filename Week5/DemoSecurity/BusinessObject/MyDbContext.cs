using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject
{
    public class MyDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public MyDbContext()
        {
        }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public static DbContextOptions<MyDbContext> GetOptions()
        {
            DbContextOptionsBuilder<MyDbContext> optionsBuilder = new();
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DemoDb"));
            return optionsBuilder.Options;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
