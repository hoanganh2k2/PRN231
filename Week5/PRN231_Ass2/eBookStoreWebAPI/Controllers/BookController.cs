using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStoreWebAPI.Controllers
{
    public class BookController : ODataController
    {
        private readonly IBookService _bookService;

        public BookController()
        {
            _bookService = new BookService();
        }

        [EnableQuery]
        public async Task<IQueryable<Book>> Get()
        {
            IQueryable<Book> books = await _bookService.GetAllBooksAsync();
            return books.AsQueryable();
        }

        public async Task<IActionResult> Post([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await _bookService.AddBookAsync(book);

            if (result)
            {
                return Created(book);
            }
            else
            {
                return BadRequest("User creation failed.");
            }
        }
        [HttpPut("Book")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _bookService.UpdateBookAsync(book);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        public async Task<IActionResult> Delete(int key)
        {
            Book book = await _bookService.GetBookByIdAsync(key);
            if (book == null)
            {
                return NotFound();
            }

            await _bookService.DeleteBookAsync(key);

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IEnumerable<Book>> Search(string keyword)
        {
            return await _bookService.SearchBooksByTitleAsync(keyword);
        }
    }
}
