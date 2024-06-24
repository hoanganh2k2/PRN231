using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStoreWebAPI.Controllers
{
    public class PublisherController : ODataController
    {
        private readonly IPublisherService _publisherService;

        public PublisherController()
        {
            _publisherService = new PublisherService();
        }

        [EnableQuery]
        public async Task<IQueryable<Publisher>> Get()
        {
            IQueryable<BusinessObject.Models.Publisher> publishers = await _publisherService.GetAllPublishersAsync();
            return publishers.AsQueryable();
        }

        // Other CRUD actions without [EnableQuery]

        public async Task<IActionResult> Post([FromBody] Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _publisherService.AddPublisherAsync(publisher);

            return Created(publisher);
        }
        [HttpPut("Publisher")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _publisherService.UpdatePublisherAsync(publisher);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        public async Task<IActionResult> Delete(int key)
        {
            BusinessObject.Models.Publisher publisher = await _publisherService.GetPublisherByIdAsync(key);
            if (publisher == null)
            {
                return NotFound();
            }

            await _publisherService.DeletePublisherAsync(key);

            return NoContent();
        }
    }
}
