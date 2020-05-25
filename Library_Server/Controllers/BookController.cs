using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library_Models;
using WebAPI_Server.Repositories;

namespace WebAPI_Server.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {

        /* -------------------- */
        /*      SIMPLE GET      */
        /* -------------------- */

        //Send all book's data.
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var books = BookRepository.GetBooks();
            return Ok(books);
        }

        //Send a specific book's data by it's id.
        [HttpGet("{id}")]
        public ActionResult<Book> GetById(long id)
        {
            var book = BookRepository.GetBook(id);
            //Check successs
            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound();
            }
        }

        /* ------------------------- */
        /*      SEARCH WITH GET      */
        /* ------------------------- */
        
        //Search books by their code.
        [HttpGet("searchByCode/{code}")]
        public ActionResult<IEnumerable<Book>> SearchByCode(string code)
        {
            var books = BookRepository.SearchBookByExpression(b => b.Code.Contains(code));
            //Check successs
            if (books != null)
            {
                return Ok(books);
            }
            else
            {
                return NotFound();
            }
        }

        //Search books by their code.
        [HttpGet("searchByAuthor/{id}/{author}")]
        public ActionResult<IEnumerable<Book>> SearchByAuthor(long id, string author)
        {
            var books = BookRepository.SearchBookByExpression(b => b.Author.Contains(author) && b.BorrowerId.Value.Equals(id));
            //Check successs
            if (books != null)
            {
                return Ok(books);
            }
            else
            {
                return NotFound();
            }
        }

        //Search books by their Title.
        [HttpGet("searchByTitle/{id}/{title}")]
        public ActionResult<IEnumerable<Book>> SearchByTitle(long id, string title)
        {
            var books = BookRepository.SearchBookByExpression(b => b.Title.Contains(title) && b.BorrowerId.Value.Equals(id));
            //Check successs
            if (books != null)
            {
                return Ok(books);
            }
            else
            {
                return NotFound();
            }
        }

        //Search books by their borrower.
        [HttpGet("borrower/{borrowerId}")]
        public ActionResult<IEnumerable<Book>> SearchBorrowed(long borrowerId)
        {
            var books = BookRepository.SearchBookByExpression(b => b.BorrowerId.Value.Equals(borrowerId));
            //Check successs
            if (books != null)
            {
                return Ok(books);
            }
            else
            {
                return NotFound();
            }
        }

        /* ------------- */
        /*      POST     */
        /* ------------- */

        //Receive and store a single book's data.
        [HttpPost]
        public ActionResult Post(Book book)
        {
            BookRepository.AddBook(book);

            return Ok();
        }

        /* ------------ */
        /*      PUT     */
        /* ------------ */

        //Receive and update a single book's data.
        [HttpPut("{id}")]
        public ActionResult Put(Book book, long id)
        {
            var dbBook = BookRepository.GetBook(id);
            //Update if it exist
            if (dbBook != null) {
                BookRepository.UpdateBook(book);
                return Ok();
            }
            return NotFound();
        }

        /* ---------------- */
        /*      DELETE      */
        /* ---------------- */

        //Delete a single book's data from the server
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            var book = BookRepository.GetBook(id);
            if (book != null)
            {
                BookRepository.DeleteBook(book);
                return Ok();
            }
            return NotFound();
        }
    }
}