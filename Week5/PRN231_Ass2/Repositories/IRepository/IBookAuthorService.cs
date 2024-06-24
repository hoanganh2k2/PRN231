using BusinessObject.Models;

namespace DataAccess.IRepository
{
    public interface IBookAuthorService
    {
        Task AddBookAuthorAsync(BookAuthor bookAuthor);
        Task<BookAuthor> GetBookAuthorAsync(int bookId, int authorId);
        Task<List<BookAuthor>> GetAuthorsForBookAsync(int bookId);
        Task<IQueryable<BookAuthor>> GetAllBooksAsync();
        Task UpdateBookAuthorAsync(BookAuthor updatedBookAuthor);
        Task DeleteBookAuthorAsync(int bookId, int authorId);
    }
}
