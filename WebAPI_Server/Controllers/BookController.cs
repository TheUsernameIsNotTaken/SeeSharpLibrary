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
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var books = BookRepository.GetBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(long id)
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

        [HttpPost]
        public ActionResult Post(Book book)
        {
            BookRepository.AddBook(book);

            return Ok();
        }

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