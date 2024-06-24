using BusinessObject.Data;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class AuthorService : IAuthorService
    {
        private readonly AuthorDAO _context;
        public AuthorService()
        {
            _context = new AuthorDAO(new MyDBContext());
        }
        public Task AddAuthorAsync(Author author)
        {
            return _context.AddAuthorAsync(author);
        }

        public Task DeleteAuthorAsync(int authorId)
        {
            return (_context.DeleteAuthorAsync(authorId));
        }

        public Task<IQueryable<Author>> GetAllAuthorsAsync()
        {
            return _context.GetAllAuthorsAsync();
        }

        public Task<Author> GetAuthorByIdAsync(int authorId)
        {
            return _context.GetAuthorByIdAsync(authorId);
        }

        public Task UpdateAuthorAsync(Author updatedAuthor)
        {
            return _context.UpdateAuthorAsync(updatedAuthor);
        }
    }
}
