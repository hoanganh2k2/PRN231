using BusinessObject.Data;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class BookService : IBookService
    {
        private readonly BookDAO _dao;
        public BookService()
        {
            _dao = new BookDAO(new MyDBContext());
        }
        public Task<Boolean> AddBookAsync(Book book)
        {
            return _dao.AddBookAsync(book);
        }

        public Task DeleteBookAsync(int bookId)
        {
            return (_dao.DeleteBookAsync(bookId));
        }

        public Task<IQueryable<Book>> GetAllBooksAsync()
        {
            return _dao.GetAllBooksAsync();
        }

        public Task<Book> GetBookByIdAsync(int bookId)
        {
            return _dao.GetBookByIdAsync(bookId);
        }

        public Task<List<Book>> SearchBooksByTitleAsync(string title)
        {
            return _dao.SearchBooksByTitleAsync(title);
        }

        public Task UpdateBookAsync(Book updatedBook)
        {
            return _dao.UpdateBookAsync(updatedBook);
        }
    }
}
