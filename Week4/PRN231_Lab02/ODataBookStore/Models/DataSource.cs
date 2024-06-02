namespace ODataBookStore.Models
{
    public static class DataSource
    {
        private static IList<Book>? listBooks { get; set; }
        public static IList<Book> GetBooks()
        {
            if (listBooks != null) return listBooks;

            listBooks = new List<Book>();

            Book book = new()
            {
                Id = 1,
                ISBN = "978-0-321-87758-1",
                Title = "Essential C#5.0",
                Author = "Mark Michaelis",
                Price = 59.99m,
                Location = new Address
                {
                    City = "HCM City",
                    Street = "D2, Thu Duc District"
                },
                Press = new Press
                {
                    Id = 1,
                    Name = "Addision-Wesley",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);

            book = new()
            {
                Id = 2,
                ISBN = "978-0-321-87758-1",
                Title = "Essential C#6.0",
                Author = "Mark Michaelis",
                Price = 50.99m,
                Location = new Address
                {
                    City = "HCM City",
                    Street = "D2, Thu Duc District"
                },
                Press = new Press
                {
                    Id = 2,
                    Name = "Kim Dong",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);

            book = new()
            {
                Id = 3,
                ISBN = "978-0-321-87758-1",
                Title = "Essential C#7.0",
                Author = "Mark Michaelis",
                Price = 49.99m,
                Location = new Address
                {
                    City = "HCM City",
                    Street = "D2, Thu Duc District"
                },
                Press = new Press
                {
                    Id = 3,
                    Name = "Bao tuoi tre",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);

            book = new()
            {
                Id = 4,
                ISBN = "978-0-321-87758-1",
                Title = "Essential C#8.0",
                Author = "Mark Michaelis",
                Price = 40.99m,
                Location = new Address
                {
                    City = "HCM City",
                    Street = "D2, Thu Duc District"
                },
                Press = new Press
                {
                    Id = 4,
                    Name = "Suc Song",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);

            return listBooks;
        }
    }
}
