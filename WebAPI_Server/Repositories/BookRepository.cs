using System.Collections.Generic;
using System.Linq;
using Library_Models;

namespace WebAPI_Server.Repositories
{
    public static class BookRepository
    {
        //Get all book's data.
        public static IList<Book> GetBooks()
        {
            using(var database = new BookContext())
            {
                var books = database.Books.ToList();
                return books;
            }
        }

        //Get a specific book's data.
        public static Book GetBook(long id)
        {
            using (var database = new BookContext())
            {
                var book = database.Books.Where(b => b.Id == id).FirstOrDefault();
                return book;
            }
        }

        //Get a single book's data by
        public static Book GetBookByCode(string code)
        {
            using (var database = new BookContext())
            {
                var book = database.Books.Where(b => b.Code == code).FirstOrDefault();
                return book;
            }
        }

        //Get a list of books' data by the part of their code
        public static IList<Book> SearchBookByCode(string code)
        {
            using (var database = new BookContext())
            {
                var books = database.Books.Where(b => b.Code.Contains(code)).ToList();
                return books;
            }
        }

        //Add a single book's data.
        public static void AddBook(Book book)
        {
            using (var database = new BookContext())
            {
                database.Books.Add(book);
                database.SaveChanges();
            }
        }

        //Update a single book's data.
        public static void UpdateBook(Book book)
        {
            using (var database = new BookContext())
            {
                database.Books.Update(book);
                database.SaveChanges();
            }
        }

        //Delete a single book's data from the database.
        public static void  DeleteBook(Book book)
        {
            using (var database = new BookContext())
            {
                database.Books.Remove(book);
                database.SaveChanges();
            }
        }
    }
}
