using BusinessObject.Data;
using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class AuthorDAO
    {
        private readonly MyDBContext _context;

        public AuthorDAO(MyDBContext context)
        {
            _context = context;
        }

        // Create
        public async Task AddAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        // Read
        public async Task<Author> GetAuthorByIdAsync(int authorId)
        {
            return await _context.Authors.FindAsync(authorId);
        }

        public async Task<IQueryable<Author>> GetAllAuthorsAsync()
        {
            return _context.Authors;
        }

        // Update
        public async Task UpdateAuthorAsync(Author updatedAuthor)
        {
            Author? existingAuthor = await _context.Authors.FindAsync(updatedAuthor.AuthorId);
            if (existingAuthor != null)
            {
                existingAuthor.FirstName = updatedAuthor.FirstName;
                existingAuthor.LastName = updatedAuthor.LastName;
                existingAuthor.Phone = updatedAuthor.Phone;
                existingAuthor.Address = updatedAuthor.Address;
                existingAuthor.City = updatedAuthor.City;
                existingAuthor.State = updatedAuthor.State;
                existingAuthor.Zip = updatedAuthor.Zip;
                existingAuthor.Email = updatedAuthor.Email;

                await _context.SaveChangesAsync();
            }
        }

        // Delete
        public async Task DeleteAuthorAsync(int authorId)
        {
            BusinessObject.Models.Author? authorToDelete = await _context.Authors.FindAsync(authorId);
            if (authorToDelete != null)
            {
                _context.Authors.Remove(authorToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
