using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Models;
using Routing_AttributeRouting.Models;

namespace Routing_AttributeRouting.Controllers
{
    //建完controller後，到packeage manager console輸入Enable-Migrations
    //Configuration.cs 建立初始資料
    //packeage manager console: Add-Migration Initial、Update-Database

    //url: http://localhost:2322/books (原本是/api/books)
    [RoutePrefix("books")]
    public class BooksController : ApiController
    {
        private BookAPIContext db = new BookAPIContext();

        [Route("")]
        // GET: api/Books
        public IQueryable<Book> GetBooks()
        {
            return db.Books;
        }

        [Route("{id:int}")]
        // GET: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.BookId)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        //url: http://localhost:2322/books/溝通說話
        [Route("{genre}")]
        public IHttpActionResult GetBookByGenre(string genre)
        {
            return Ok(db.Books.Include(b => b.Author).Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)));
        }

        //url: http://localhost:2322/authors/{authorId:int}/books/ not http://localhost:2322/books/authors/{authorId:int}/books/
        [Route("~/authors/{authorId:int}/books/")]
        public IHttpActionResult GetBooksByAuthor(int authorId)
        {
            return Ok(db.Books.Include(b => b.Author).Where(b => b.AuthorId == authorId));
        }

        //url:http://localhost:2322/books/date/2016-06-27
        [Route("date/{pubdate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        // *:貪婪字元，將date後面整個當作一個參數
        [Route("date/{*pubdate:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")]
        public IHttpActionResult Get(DateTime pubdate)
        {
            return Ok(db.Books.Include(b => b.Author).Where(b => DbFunctions.TruncateTime(b.PublishDate) == DbFunctions.TruncateTime(pubdate)));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.BookId == id) > 0;
        }
    }
}