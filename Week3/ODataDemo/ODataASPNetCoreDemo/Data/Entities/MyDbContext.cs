using Microsoft.EntityFrameworkCore;

namespace ODataASPNetCoreDemo.Data.Entities
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Gadgets> Gadgets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Week3Demo"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gadgets>().HasData(
                new Gadgets { Id = 1, ProductName = "Product1", Brand = "Nhat", Cost = 10, Type = "Fruit" },
                new Gadgets { Id = 2, ProductName = "Product2", Brand = "Trung", Cost = 20, Type = "Sea food" },
                new Gadgets { Id = 3, ProductName = "Product3", Brand = "Han", Cost = 30, Type = "Food" },
                new Gadgets { Id = 4, ProductName = "Product4", Brand = "Nhat", Cost = 15, Type = "Sea food" },
                new Gadgets { Id = 5, ProductName = "Product5", Brand = "Nhat", Cost = 17, Type = "Fruit" },
                new Gadgets { Id = 6, ProductName = "Product6", Brand = "Trung", Cost = 40, Type = "Food" },
                new Gadgets { Id = 7, ProductName = "Product7", Brand = "Han", Cost = 12, Type = "Food" },
                new Gadgets { Id = 8, ProductName = "Product8", Brand = "Nhat", Cost = 18, Type = "Sea food" },
                new Gadgets { Id = 9, ProductName = "Product9", Brand = "Trung", Cost = 50, Type = "Fruit" },
                new Gadgets { Id = 10, ProductName = "Product10", Brand = "Viet Nam", Cost = 90, Type = "Sea food" }
                );
        }
    }
}
