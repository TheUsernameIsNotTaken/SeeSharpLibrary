using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library_Models;
using WebAPI_Server.Repositories;

namespace WebAPI_Server.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
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

        //Get a single book's data by it's code.
        [HttpGet("get/{code}")]
        public ActionResult<Book> GetByCode(string code)
        {
            var book = BookRepository.GetBookByCode(code);
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

        //Search books by it's code.
        [HttpGet("search/{code}")]
        public ActionResult<IEnumerable<Book>> SearchByCode(string code)
        {
            var books = BookRepository.SearchBookByCode(code);
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

        //Receive and store a single book's data.
        [HttpPost]
        public ActionResult Post(Book book)
        {
            BookRepository.AddBook(book);

            return Ok();
        }

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