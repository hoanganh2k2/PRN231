using BusinessObject.Models;

namespace DataAccess.IRepository
{
    public interface IAuthorService
    {
        Task AddAuthorAsync(Author author);
        Task<Author> GetAuthorByIdAsync(int authorId);
        Task<IQueryable<Author>> GetAllAuthorsAsync();
        Task UpdateAuthorAsync(Author updatedAuthor);
        Task DeleteAuthorAsync(int authorId);
    }
}
