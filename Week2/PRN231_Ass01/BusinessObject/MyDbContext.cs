using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Ass01DB"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Food" },
                new Category { CategoryId = 2, CategoryName = "Drinks" },
                new Category { CategoryId = 3, CategoryName = "Clothing" },
                new Category { CategoryId = 4, CategoryName = "Fruit" },
                new Category { CategoryId = 5, CategoryName = "Seafood" });
            modelBuilder.Entity<OrderDetail>().HasKey(o => new { o.ProductId, o.OrderId });

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Milk", CategoryId = 2, ProductImage = "https://www.vinamilk.com.vn/sua-tuoi-vinamilk/wp-content/themes/suanuoc/tpl/trang-chu/images/img_1-mb.jpg", UnitPrice = 30, UnitsInStock = 4, Weight = 0.5 },
                new Product { ProductId = 2, ProductName = "Pizza", CategoryId = 1, ProductImage = "https://img.dominos.vn/cach-nuong-pizza-chuan-0.jpg", UnitPrice = 30, UnitsInStock = 1, Weight = 0.3 },
                new Product { ProductId = 3, ProductName = "Banana", CategoryId = 4, ProductImage = "https://cdn-prod.medicalnewstoday.com/content/images/articles/271/271157/bananas-chopped-up-in-a-bowl.jpg", UnitPrice = 15, UnitsInStock = 2, Weight = 0.2 },
                new Product { ProductId = 4, ProductName = "Pants", CategoryId = 3, ProductImage = "https://bizweb.dktcdn.net/100/438/408/files/cargo-pants-yodyvn.jpg?v=1670222023662", UnitPrice = 100, UnitsInStock = 1, Weight = 0.2 },
                new Product { ProductId = 5, ProductName = "Noodles", CategoryId = 1, ProductImage = "https://www.recipetineats.com/wp-content/uploads/2023/09/Garlic-noodles-with-egg-close-up.jpg", UnitPrice = 45, UnitsInStock = 1, Weight = 0.3 }
                );
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
