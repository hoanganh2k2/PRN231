using BusinessObject.Data;
using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class PublisherDAO
    {
        private readonly MyDBContext _context;

        public PublisherDAO(MyDBContext context)
        {
            _context = context;
        }

        // Create
        public async Task AddPublisherAsync(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
        }

        // Read
        public async Task<Publisher> GetPublisherByIdAsync(int publisherId)
        {
            return await _context.Publishers.FindAsync(publisherId);
        }

        public async Task<IQueryable<Publisher>> GetAllPublishersAsync()
        {
            return _context.Publishers;
        }

        // Update
        public async Task UpdatePublisherAsync(Publisher updatedPublisher)
        {
            Publisher? existingPublisher = await _context.Publishers.FindAsync(updatedPublisher.PublisherId);
            if (existingPublisher != null)
            {
                existingPublisher.PublisherName = updatedPublisher.PublisherName;
                existingPublisher.City = updatedPublisher.City;
                existingPublisher.Country = updatedPublisher.Country;
                existingPublisher.State = updatedPublisher.State;

                await _context.SaveChangesAsync();
            }
        }

        // Delete
        public async Task DeletePublisherAsync(int publisherId)
        {
            BusinessObject.Models.Publisher? publisherToDelete = await _context.Publishers.FindAsync(publisherId);
            if (publisherToDelete != null)
            {
                _context.Publishers.Remove(publisherToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
