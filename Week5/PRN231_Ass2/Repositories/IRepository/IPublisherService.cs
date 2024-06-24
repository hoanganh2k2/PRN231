using BusinessObject.Models;

namespace DataAccess.IRepository
{
    public interface IPublisherService
    {
        Task AddPublisherAsync(Publisher publisher);
        Task<Publisher> GetPublisherByIdAsync(int publisherId);
        Task DeletePublisherAsync(int publisherId);
        Task<IQueryable<Publisher>> GetAllPublishersAsync();
        Task UpdatePublisherAsync(Publisher updatedPublisher);

    }
}
