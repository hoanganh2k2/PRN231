using BusinessObject.Data;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class PublisherService : IPublisherService
    {
        private readonly PublisherDAO _dao;
        public PublisherService()
        {
            _dao = new PublisherDAO(new MyDBContext());
        }
        public Task AddPublisherAsync(Publisher publisher)
        {
            return _dao.AddPublisherAsync(publisher);
        }

        public Task DeletePublisherAsync(int publisherId)
        {
            return (_dao.DeletePublisherAsync(publisherId));
        }

        public Task<IQueryable<Publisher>> GetAllPublishersAsync()
        {
            return _dao.GetAllPublishersAsync();
        }

        public Task<Publisher> GetPublisherByIdAsync(int publisherId)
        {
            return _dao.GetPublisherByIdAsync(publisherId);
        }

        public Task UpdatePublisherAsync(Publisher updatedPublisher)
        {
            return _dao.UpdatePublisherAsync(updatedPublisher);
        }
    }
}
