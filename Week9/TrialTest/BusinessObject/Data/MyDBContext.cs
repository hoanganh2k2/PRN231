using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Data
{
	public class MyDBContext : DbContext
	{
		public MyDBContext()
		{
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			IConfigurationBuilder builder = new ConfigurationBuilder()
						.SetBasePath(Directory.GetCurrentDirectory())
						.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
			IConfigurationRoot configuration = builder.Build();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("Trial"));
		}

		public DbSet<Address> Addresses { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Address>().HasData(
				new Address { AddressId = 1, City = "Hà Tĩnh", AddressName = "Kỳ Anh" },
				new Address { AddressId = 2, City = "Đà Nẵng", AddressName = "Trung Khánh" },
				new Address { AddressId = 3, City = "Huế", AddressName = "Chợ" });

			modelBuilder.Entity<User>().HasData(
				new User { UserId = 1, Name = "Hoàng Anh", AddressId = 1 },
				new User { UserId = 2, Name = "Hoàng Hà", AddressId = 2 },
				new User { UserId = 3, Name = "Hoàng Ánh", AddressId = 3 },
				new User { UserId = 4, Name = "Hoàng Minh", AddressId = 1 });



			modelBuilder.Entity<Product>().HasData(
				new Product { ProductId = 1, ProductName = "Bánh Cu Đơ", Description = "Ngon", Price = 50000, UserId = 1 },
				new Product { ProductId = 2, ProductName = "Bánh Cốm", Description = "Ngon", Price = 30000, UserId = 2 },
				new Product { ProductId = 3, ProductName = "Bánh Dập", Description = "Ngon", Price = 40000, UserId = 3 },
				new Product { ProductId = 4, ProductName = "Bánh Trôi Nước", Description = "Ngon", Price = 20000, UserId = 4 },
				new Product { ProductId = 5, ProductName = "Bánh Tày", Description = "Ngon", Price = 10000, UserId = 1 });
		}
	}
}
