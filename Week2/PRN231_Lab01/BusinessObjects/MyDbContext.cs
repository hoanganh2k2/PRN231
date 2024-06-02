using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Lab01DB"));
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder optionsBuilder)
        {
            optionsBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Food" },
                new Category { CategoryId = 2, CategoryName = "Drinks" },
                new Category { CategoryId = 3, CategoryName = "Clothes" },
                new Category { CategoryId = 4, CategoryName = "Fruit" }
                );

            optionsBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Milk", CategoryId = 2, UnitsInStock = 4, UnitPrice = 100 },
                new Product { ProductId = 2, ProductName = "Pizza", CategoryId = 1, UnitsInStock = 8, UnitPrice = 200 },
                new Product { ProductId = 3, ProductName = "Banana", CategoryId = 4, UnitsInStock = 5, UnitPrice = 70 },
                new Product { ProductId = 4, ProductName = "Pants", CategoryId = 3, UnitsInStock = 1, UnitPrice = 400 },
                new Product { ProductId = 5, ProductName = "Juice", CategoryId = 2, UnitsInStock = 3, UnitPrice = 80 },
                new Product { ProductId = 6, ProductName = "Noodles", CategoryId = 1, UnitsInStock = 1, UnitPrice = 80 },
                new Product { ProductId = 7, ProductName = "Orange", CategoryId = 4, UnitsInStock = 3, UnitPrice = 60 }
                );
        }
    }
}
