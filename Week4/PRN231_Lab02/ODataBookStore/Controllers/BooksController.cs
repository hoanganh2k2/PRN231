using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using ODataBookStore.Models;

namespace ODataBookStore.Controllers
{
    public class BooksController : ControllerBase
    {
        private readonly BookStoreContext db;

        public BooksController(BookStoreContext context)
        {
            db = context;
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            if (context.Books.Count() == 0)
            {
                foreach (Book b in DataSource.GetBooks())
                {
                    context.Books.Add(b);
                    context.Presses.Add(b.Press);
                }
                context.SaveChanges();
            }
        }

        [EnableQuery(PageSize = 100)]
        public IActionResult Get()
        {
            return Ok(db.Books);
        }

        [EnableQuery]
        public IActionResult Get(int key, string version)
        {
            return Ok(db.Books.FirstOrDefault(c => c.Id == key));
        }

        [EnableQuery]
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book cannot be null.");
            }

            db.Books.Add(book);
            db.SaveChanges();
            return Ok(book);
        }

        [EnableQuery]
        public IActionResult Delete(int key)
        {
            Book? b = db.Books.FirstOrDefault(b => b.Id == key);

            if (b == null) return NotFound();

            db.Books.Remove(b);
            db.SaveChanges();
            return Ok();
        }

        [EnableQuery]
        public IActionResult Put(int key, [FromBody] Book book)
        {
            if (book == null || key != book.Id) return BadRequest();

            Book? book1 = db.Books.FirstOrDefault(b => b.Id == key);

            book.Location = book1.Location;
            book.Press = book1.Press;

            db.Books.Update(book);
            db.SaveChanges();
            return Ok();
        }
    }
}
