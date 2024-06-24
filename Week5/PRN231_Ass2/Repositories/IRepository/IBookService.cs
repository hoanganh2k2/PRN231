using BusinessObject.Models;

namespace DataAccess.IRepository
{
    public interface IBookService
    {
        Task<Boolean> AddBookAsync(Book book);
        Task<Book> GetBookByIdAsync(int bookId);
        Task DeleteBookAsync(int bookId);
        Task<IQueryable<Book>> GetAllBooksAsync();
        Task UpdateBookAsync(Book updatedBook);
        Task<List<Book>> SearchBooksByTitleAsync(string title);
    }
}
