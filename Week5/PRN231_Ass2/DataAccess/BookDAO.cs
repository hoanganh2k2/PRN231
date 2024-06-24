using BusinessObject.Data;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class BookDAO
    {
        private readonly MyDBContext _context;

        public BookDAO(MyDBContext context)
        {
            _context = context;
        }

        // Create
        public async Task<Boolean> AddBookAsync(Book book)
        {
            try
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        // Read
        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _context.Books.FindAsync(bookId);
        }

        public async Task<IQueryable<Book>> GetAllBooksAsync()
        {
            return _context.Books;
        }

        // Update
        public async Task UpdateBookAsync(Book updatedBook)
        {
            Book? existingBook = await _context.Books.FindAsync(updatedBook.BookId);
            if (existingBook != null)
            {
                existingBook.Title = updatedBook.Title;
                existingBook.PublisherId = updatedBook.PublisherId;
                existingBook.Type = updatedBook.Type;
                existingBook.Price = updatedBook.Price;
                existingBook.Advance = updatedBook.Advance;
                existingBook.Loyalty = updatedBook.Loyalty;
                existingBook.Sales = updatedBook.Sales;
                existingBook.Notes = updatedBook.Notes;
                existingBook.PublishDate = updatedBook.PublishDate;
                _context.Books.Update(existingBook);
                await _context.SaveChangesAsync();
            }
        }

        // Delete
        public async Task DeleteBookAsync(int bookId)
        {
            BusinessObject.Models.Book? bookToDelete = await _context.Books.FindAsync(bookId);
            if (bookToDelete != null)
            {
                _context.Books.Remove(bookToDelete);
                await _context.SaveChangesAsync();
            }
        }

        // Search Books by Title
        public async Task<List<Book>> SearchBooksByTitleAsync(string title)
        {
            return await _context.Books
                .Where(book => book.Title.Contains(title))
                .ToListAsync();
        }
    }
}
