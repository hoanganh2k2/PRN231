using BusinessObject.Data;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class BookAuthorService : IBookAuthorService
    {
        private readonly BookAuthorDAO _dao;
        public BookAuthorService()
        {
            _dao = new BookAuthorDAO(new MyDBContext());
        }
        public Task AddBookAuthorAsync(BookAuthor bookAuthor)
        {
            return _dao.AddBookAuthorAsync(bookAuthor);
        }

        public Task DeleteBookAuthorAsync(int bookId, int authorId)
        {
            return _dao.DeleteBookAuthorAsync(bookId, authorId);
        }

        public Task<List<BookAuthor>> GetAuthorsForBookAsync(int bookId)
        {
            return _dao.GetAuthorsForBookAsync(bookId);
        }

        public Task<BookAuthor> GetBookAuthorAsync(int bookId, int authorId)
        {
            return _dao.GetBookAuthorAsync(bookId, authorId);
        }

        public Task UpdateBookAuthorAsync(BookAuthor updatedBookAuthor)
        {
            return _dao.UpdateBookAuthorAsync(updatedBookAuthor);
        }

        public Task<IQueryable<BookAuthor>> GetAllBooksAsync()
        {
            return _dao.GetAllBooksAsync();
        }
    }
}
