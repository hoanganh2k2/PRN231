using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStoreWebAPI.Controllers
{
    public class AuthorController : ODataController
    {
        private readonly IAuthorService _context;
        public AuthorController()
        {
            _context = new AuthorService();
        }

        [EnableQuery]
        public async Task<IQueryable<Author>> Get()
        {
            IQueryable<BusinessObject.Models.Author> authors = await _context.GetAllAuthorsAsync();
            return authors.AsQueryable();
        }


        [EnableQuery]
        public SingleResult<Author> Get(int key)
        {
            BusinessObject.Models.Author author = _context.GetAuthorByIdAsync(key).Result;

            if (author == null)
            {
                return SingleResult.Create(Enumerable.Empty<Author>().AsQueryable());
            }

            return SingleResult.Create(new[] { author }.AsQueryable());
        }


        public async Task<IActionResult> Post([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.AddAuthorAsync(author);

            return Created(author);
        }
        [HttpPut("Author")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _context.UpdateAuthorAsync(author);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            BusinessObject.Models.Author author = await _context.GetAuthorByIdAsync(key);
            if (author == null)
            {
                return NotFound();
            }

            await _context.DeleteAuthorAsync(key);

            return NoContent();
        }
    }
}
