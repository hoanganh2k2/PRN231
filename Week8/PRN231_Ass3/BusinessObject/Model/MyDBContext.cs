using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Model
{
    public class MyDBContext : IdentityDbContext<Customer>
    {
        public MyDBContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Ass3Db"));
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderDetail>()
                .HasKey(r => new { r.OrderID, r.ProductID });

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, CategoryName = "Food" },
                new Category { CategoryID = 2, CategoryName = "Clothes" },
                new Category { CategoryID = 3, CategoryName = "Flower" },
                new Category { CategoryID = 4, CategoryName = "Candy" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    ProductID = 1,
                    ProductName = "Red Rose",
                    Weight = 0.5,
                    UnitPrice = 100000,
                    UnitsInStock = 100,
                    CategoryID = 4,
                },
                new Product()
                {
                    ProductID = 2,
                    ProductName = "Noodle",
                    Weight = 0.2,
                    UnitPrice = 30000,
                    UnitsInStock = 1,
                    CategoryID = 1,
                },
                new Product()
                {
                    ProductID = 3,
                    ProductName = "Shirt",
                    Weight = 0.1,
                    UnitPrice = 70000,
                    UnitsInStock = 2,
                    CategoryID = 2,
                }
                );
        }
    }
}
