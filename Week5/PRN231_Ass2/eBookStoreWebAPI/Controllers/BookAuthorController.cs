using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStoreWebAPI.Controllers
{
    public class BookAuthorController : ODataController
    {
        private readonly IBookAuthorService _bookAuthorService;

        public BookAuthorController()
        {
            _bookAuthorService = new BookAuthorService();
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            IQueryable<BusinessObject.Models.BookAuthor> books = await _bookAuthorService.GetAllBooksAsync();
            return Ok(books);
        }
        /*        [HttpGet("ById")]
                public ActionResult<BookAuthor> Get( int bookId, int authorId)
                {
                    var bookAuthor = _bookAuthorService.GetBookAuthorAsync(bookId, authorId).Result;

                    if (bookAuthor == null)
                    {
                        return NoContent();
                    }

                    return Ok(bookAuthor);
                }*/

        public async Task<IActionResult> Post([FromBody] BookAuthor bookAuthor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _bookAuthorService.AddBookAuthorAsync(bookAuthor);

            return Created(bookAuthor);
        }
        [HttpPut("BookAuthor")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int authorId, BookAuthor bookAuthor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _bookAuthorService.UpdateBookAuthorAsync(bookAuthor);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        public async Task<IActionResult> Delete([FromODataUri] int bookId, [FromODataUri] int authorId)
        {
            BusinessObject.Models.BookAuthor bookAuthor = await _bookAuthorService.GetBookAuthorAsync(bookId, authorId);
            if (bookAuthor == null)
            {
                return NotFound();
            }

            await _bookAuthorService.DeleteBookAuthorAsync(bookId, authorId);

            return NoContent();
        }
    }
}
