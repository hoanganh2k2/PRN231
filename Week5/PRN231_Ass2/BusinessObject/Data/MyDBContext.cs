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
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Ass2Db"));
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleDesc = "Admin" },
                new Role { RoleId = 2, RoleDesc = "Customer" });

            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, FirstName = "Nguyen", LastName = "Nhat Anh", Address = "Ky Dong, Ky Anh, Ha Tinh", City = "Ha Tinh", Email = "Nhatanh@gmail.com", Phone = "0374290857", State = "true", Zip = "ok" },
                new Author { AuthorId = 2, FirstName = "Nguyen", LastName = "Duc Trong", Address = "Dien Ngoc, Dien Ban, Quang Nam", City = "Quang Nam", Email = "Ductrong@gmail.com", Phone = "0374208965", State = "true", Zip = "okluon" },
                new Author { AuthorId = 3, FirstName = "Miaky", LastName = "Sugaru", Address = "Ngu Hanh Son, Da Nang", City = "Da Nang", Email = "Miaky@gmail.com", Phone = "0374278967", State = "true", Zip = "ok" });

            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { PublisherId = 1, PublisherName = "New Moon Books", City = "Da Nang", Country = "Viet Nam", State = "Ok" },
                new Publisher { PublisherId = 2, PublisherName = "Binet ", City = "Ha Tinh", Country = "Viet Nam", State = "Ok" },
                new Publisher { PublisherId = 3, PublisherName = "Nha sach tre", City = "Quang Nam", Country = "Viet Nam", State = "Ok" },
                new Publisher { PublisherId = 4, PublisherName = "Algoda", City = "Newyork", Country = "America", State = "Ok" }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Trinh tham", Type = "Tieu thuyet", Price = 130, Advance = "true", PublishDate = new DateTime(2023, 6, 6), PublisherId = 1, Loyalty = "true", Notes = "", Sales = 10 },
                new Book { BookId = 2, Title = "Tinh Cam", Type = "Truyen dai", Price = 100, Advance = "true", PublishDate = new DateTime(2023, 7, 4), PublisherId = 2, Loyalty = "true", Notes = "", Sales = 10 },
                new Book { BookId = 3, Title = "Hai huoc", Type = "Truyen ngan", Price = 50, Advance = "true", PublishDate = new DateTime(2023, 10, 5), PublisherId = 3, Loyalty = "true", Notes = "", Sales = 10 }
                );

            modelBuilder.Entity<BookAuthor>().HasData(
                new BookAuthor { AuthorId = 1, BookId = 1, AuthorOrder = 1, RoyaltyPercentage = 70 },
                new BookAuthor { AuthorId = 1, BookId = 2, AuthorOrder = 1, RoyaltyPercentage = 35 },
                new BookAuthor { AuthorId = 2, BookId = 2, AuthorOrder = 2, RoyaltyPercentage = 35 },
                new BookAuthor { AuthorId = 3, BookId = 3, AuthorOrder = 1, RoyaltyPercentage = 80 }
                );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, FirstName = "Nguyen", MiddleName = "Hoang", LastName = "Anh", Email = "anhkd2002@gmail.com", Password = "123456", RoleId = 2, Source = "true", PublisherId = 1 });
        }
    }
}
