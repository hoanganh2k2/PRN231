using BusinessObject.Data;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class BookAuthorDAO
    {
        private readonly MyDBContext _context;

        public BookAuthorDAO(MyDBContext context)
        {
            _context = context;
        }

        // Create
        public async Task AddBookAuthorAsync(BookAuthor bookAuthor)
        {
            _context.BookAuthors.Add(bookAuthor);
            await _context.SaveChangesAsync();
        }

        // Read
        public async Task<BookAuthor> GetBookAuthorAsync(int bookId, int authorId)
        {
            return await _context.BookAuthors
                .SingleOrDefaultAsync(ba => ba.BookId == bookId && ba.AuthorId == authorId);
        }

        public async Task<IQueryable<BookAuthor>> GetAllBooksAsync()
        {
            return _context.BookAuthors;
        }

        public async Task<List<BookAuthor>> GetAuthorsForBookAsync(int bookId)
        {
            return await _context.BookAuthors
                .Where(ba => ba.BookId == bookId)
                .ToListAsync();
        }

        // Update
        public async Task UpdateBookAuthorAsync(BookAuthor updatedBookAuthor)
        {
            BookAuthor? existingBookAuthor = await _context.BookAuthors
                .SingleOrDefaultAsync(ba => ba.BookId == updatedBookAuthor.BookId && ba.AuthorId == updatedBookAuthor.AuthorId);

            if (existingBookAuthor != null)
            {
                existingBookAuthor.AuthorOrder = updatedBookAuthor.AuthorOrder;
                existingBookAuthor.RoyaltyPercentage = updatedBookAuthor.RoyaltyPercentage;

                await _context.SaveChangesAsync();
            }
        }

        // Delete
        public async Task DeleteBookAuthorAsync(int bookId, int authorId)
        {
            BookAuthor? bookAuthorToDelete = await _context.BookAuthors
                .SingleOrDefaultAsync(ba => ba.BookId == bookId && ba.AuthorId == authorId);

            if (bookAuthorToDelete != null)
            {
                _context.BookAuthors.Remove(bookAuthorToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
